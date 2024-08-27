namespace Exapt.Wrappers;

public class Solution : NonStaticWrapper<Solution>
{
    public PuzzleId PuzzleId => new(Call("#=q0bXK6vTpnQXmi0XWqqn0yA==")!);

    public IEnumerable<SolutionExa> SolutionExas
    {
        get
        {
            IEnumerable<object> inners = Enumerable.Cast<object>(
                (System.Collections.IEnumerable)Get("#=qzJMkmdeH1slKh8PKZTwTEA==")!
            );
            return inners.Select(e => new SolutionExa(e));
        }
    }

    static Solution()
    {
        SetWrappedType("Solution");
    }

    internal Solution(object inner)
        : base(inner) { }

    public static Solution? FromFile(string filename)
    {
        Maybe solution = new(CallStatic("#=qrAtvddUJvCjJyuYaeXTtoA==", filename)!);
        return solution.IsSome() ? new Solution(solution.Unwrap()) : null;
    }
}
