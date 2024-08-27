using System.Diagnostics.CodeAnalysis;

namespace Exapt.Wrappers;

public class Puzzles : StaticWrapper<Puzzles>
{
    private static bool initialized = false;

    static Puzzles()
    {
        SetWrappedType("Puzzles");
    }

    public static void Initialize(string exapunksDirectory)
    {
        if (!initialized)
        {
            Utils.WithWorkingDirectory(exapunksDirectory, () => CallStatic("#=qmvy28H$Ax5rPhkJqrGFbUg=="));
            initialized = true;
        }
    }

    public static Puzzle FromId([NotNull] PuzzleId puzzleId)
    {
        return new Puzzle(CallStatic("#=q4YjDHfJYS2sN7OymGOsg4Q==", puzzleId.Inner)!);
    }
}
