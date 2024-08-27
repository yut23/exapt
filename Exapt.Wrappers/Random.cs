namespace Exapt.Wrappers;

public class Random(ulong seed) : NonStaticWrapper<Random>(CallConstructor(seed)!)
{
    static Random()
    {
        SetWrappedType("ReliableRandom");
    }
}
