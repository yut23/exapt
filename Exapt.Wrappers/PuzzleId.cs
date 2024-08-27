namespace Exapt.Wrappers;

public class PuzzleId : NonStaticWrapper<PuzzleId>
{
    public string Id => (string)Get("ID")!;

    static PuzzleId()
    {
        SetWrappedType("Puzzle");
    }

    internal PuzzleId(object inner)
        : base(inner) { }

    public PuzzleId(string id)
        : base(CallConstructor(id)!) { }
}
