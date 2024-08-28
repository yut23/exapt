using System.Reflection;
using Newtonsoft.Json;

namespace Exapt;

public static class Program
{
    private static void Main(string[] args)
    {
        Initialize(args[0]);

        SolutionData result = Simulate(args[1]);
        Console.WriteLine(JsonConvert.SerializeObject(result));
    }

    public static void Initialize(string exapunksDirectory)
    {
        exapunksDirectory = Path.GetFullPath(exapunksDirectory);

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

        Wrappers.Globals.SetRandom(new Wrappers.Random(1));
        Wrappers.Strings.Initialize(exapunksDirectory);
        Wrappers.Puzzles.Initialize(exapunksDirectory);
    }

    public static SolutionData Simulate(string solutionFile)
    {
        Solution solution = new(solutionFile);

        bool failed = false;
        int worstCycles = 0;
        int worstActivity = 0;
        for (int testIndex = 0; testIndex < 100; testIndex++)
        {
            Simulation simulation = solution.CreateSimulation(testIndex);

            for (int i = 0; i < 999999 && !simulation.Completed; i++)
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
        return new SolutionData()
        {
            PuzzleId = solution.PuzzleId,
            Statistics = failed
                ? null
                : new SolutionStatistics
                {
                    Cycles = worstCycles,
                    Size = solution.Size,
                    Activity = worstActivity,
                },
        };
    }
}
