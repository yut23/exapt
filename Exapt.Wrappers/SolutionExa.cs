namespace Exapt.Wrappers;

public class SolutionExa : NonStaticWrapper<SolutionExa>
{
    public Code Code
    {
        get
        {
            object a = Get("#=qspUergJPSYLfh2YdnSsRQ0oECwKCGjIWlINxFjteWhs=")!;
            object b = Utils.Get(a, "#=qm2WdvgSYgJdwJfJhUUVLTA==")!;
            object c = Utils.Get(b, "#=qxVFmzYr3PSpuzJKbb9hW3g==")!;
            return new Code(c);
        }
    }

    internal SolutionExa(object inner)
        : base(inner) { }
}
