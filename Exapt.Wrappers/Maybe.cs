namespace Exapt.Wrappers;

internal sealed class Maybe : NonStaticWrapper<Maybe>
{
    internal Maybe(object inner)
        : base(inner) { }

    public bool IsSome()
    {
        return (bool)Call("#=qgQ5O1bL8ilWB5y5e4ATciQ==")!;
    }

    public object Unwrap()
    {
        return Call("#=qmoDLSpl1snp9jPcvZqncJg==")!;
    }
}
