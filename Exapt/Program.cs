// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;

namespace Exapt;

public static class Program
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    private sealed class Arguments
    {
        [Value(
            0,
            MetaName = "solution-file",
            HelpText = "The path to the solution file to be validated.",
            Required = true
        )]
        public required string SolutionFilepath { get; set; }

        [Option(
            'e',
            "exapunks-directory",
            HelpText = "A path to the root directory of the 2022-10-13 version of Exapunks",
            Required = true
        )]
        public required string ExapunksDirectory { get; set; }

        [Option(
            't',
            "timeout",
            HelpText = "Cycles to run before timeout",
            Default = 999999
        )]
        public required int Timeout { get; set; }
    }
#pragma warning restore CA1812 // Avoid uninstantiated internal classes

    private static void Main(string[] args)
    {
        Parser parser = new(p => p.HelpWriter = null);
        ParserResult<Arguments> parserResult = parser.ParseArguments<Arguments>(args);
        _ = parserResult
            .WithParsed(InnerMain)
            .WithNotParsed(_ =>
                Console.Error.Write(
                    HelpText.AutoBuild(
                        parserResult,
                        h =>
                        {
                            h.AdditionalNewLineAfterOption = false;
                            return h;
                        }
                    )
                )
            );
        parser.Dispose();
    }

    private static void InnerMain(Arguments arguments)
    {
        Initialize(arguments.ExapunksDirectory);

        SolutionData result = Simulate(arguments.SolutionFilepath, arguments.ExapunksDirectory, arguments.Timeout);
        Console.WriteLine(JsonConvert.SerializeObject(result));
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // note: without setting LD_LIBRARY_PATH, these will resolve to the
            // system libraries, not the ones under {exapunksDirectory}/lib64
            string? mappedName = libraryName switch
            {
                "SDL2.dll" => "libSDL2-2.0.so.0",
                "SDL2_image.dll" => "libSDL2_image-2.0.so.0",
                "SDL2_mixer.dll" => "libSDL2_mixer-2.0.so.0",
                "libvorbisfile.dll" => "libvorbisfile.so.3",
                _ => null,
            };
            return mappedName is not null
                ? NativeLibrary.Load(mappedName, assembly, searchPath)
                : IntPtr.Zero;
        }

        // Otherwise, fallback to default import resolver.
        return IntPtr.Zero;
    }

    public static void Initialize(string exapunksDirectory)
    {
        // EXAPUNKS assumes numbers are formatted with dots, not commas
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        string currentDirectory = Directory.GetCurrentDirectory();
        exapunksDirectory = Path.GetFullPath(exapunksDirectory);
        Directory.SetCurrentDirectory(exapunksDirectory);

        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {
            string? assemblyFileName = args.Name.Split(',')[0] switch
            {
                "Burbank" => "EXAPUNKS.exe",
                "Steamworks.NET" => "Steamworks.NET.dll",
                _ => null,
            };
            return assemblyFileName is not null
                ? Assembly.LoadFile(Path.Join(exapunksDirectory, assemblyFileName))
                : null;
        };
        NativeLibrary.SetDllImportResolver(Assembly.Load("Burbank"), DllImportResolver);

        // apply patches before doing any initialization
        HarmonyLib.Harmony harmony = new(nameof(Program));
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Wrappers.Globals.SetRandom(new Wrappers.Random(1));
        Wrappers.Strings.Initialize();
        Wrappers.Puzzles.Initialize();
        Wrappers.GameLogic.Instance = new Wrappers.GameLogic()
        {
            // I don't think this file is ever written
            Config = new Wrappers.Config("config.cfg")
        };
        try
        {
            Wrappers.Renderer.Initialize(Wrappers.RendererType.Direct3D, false);
        }
        catch (DllNotFoundException)
        {
            // loading D3D failed, try OpenGL instead
            Wrappers.Renderer.Initialize(Wrappers.RendererType.OpenGl, false);
            // note, this window won't actually be displayed, as the HIDDEN flag is
            // passed to SDL_WindowCreate by default
            Wrappers.GameLogic.Instance.CreateWindow("exapt", 640, 480, 0);
        }
        Wrappers.GameLogic.Instance.InitializeFontsA(() => { });
        Wrappers.GameLogic.Instance.InitializeFontsB();

        Directory.SetCurrentDirectory(currentDirectory);
    }

    public static SolutionData Simulate(string solutionFile, string exapunksDirectory, int timeout)
    {
        Solution solution = new(solutionFile);
        bool failed = false;
        int worstCycles = 0;
        int? codeSize = null;
        int worstActivity = 0;

        string currentDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(exapunksDirectory);

        try
        {
            Simulation.UseOptimizationsForPuzzle(solution.PuzzleId);

            for (int testIndex = 0; testIndex < 100; testIndex++)
            {
                Simulation simulation = solution.CreateSimulation(testIndex);
                codeSize ??= simulation.CodeSize;

                for (int i = 0; i < timeout && !simulation.Completed; i++)
                {
                    simulation.Step();
                }

                if (simulation.Completed)
                {
                    worstCycles = Math.Max(worstCycles, simulation.Cycles);
                    worstActivity = Math.Max(worstActivity, simulation.Activity);
                }
                else
                {
                    failed = true;
                    break;
                }
            }
        }
        finally
        {
            Directory.SetCurrentDirectory(currentDirectory);
        }

        return new SolutionData()
        {
            PuzzleId = solution.PuzzleId,
            Statistics = failed ? null : new SolutionStatistics
            {
                Cycles = worstCycles,
                Size = codeSize ?? throw new UnreachableException(),
                Activity = worstActivity,
            },
        };
    }
}
