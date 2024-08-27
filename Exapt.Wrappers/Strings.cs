namespace Exapt.Wrappers;

public class Strings : StaticWrapper<Strings>
{
    private static bool initialized = false;

    static Strings()
    {
        SetWrappedType("#=q$q_5RIWNSfdbsTA5NIdJHA==");
    }

    public static void Initialize(string exapunksDirectory)
    {
        if (!initialized)
        {
            Utils.WithWorkingDirectory(exapunksDirectory, () => CallStatic("#=q2IcpkAiRPnhyxP4lHls68w=="));
            initialized = true;
        }
    }
}
