using System.Diagnostics.CodeAnalysis;

namespace Exapt.Wrappers;

public class Globals : StaticWrapper<Globals>
{
    static Globals()
    {
        SetWrappedType("#=q$O9G2KbklYQ6FwBlZ$EShQ==");
    }

    public static void SetRandom([NotNull] Random random)
    {
        SetStatic("#=qyD9BljpzrnFagKpRdzAf5A==", random.Inner);
    }
}
