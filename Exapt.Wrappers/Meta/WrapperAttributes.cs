using System.Diagnostics;

namespace Exapt.Wrappers.Meta;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ClassWrapperAttribute(string innerClassName) : Attribute
{
    public string InnerClassName { get; private set; } = innerClassName;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class MethodWrapperAttribute(string innerMethodName) : Attribute
{
    public string InnerMethodName { get; private set; } = innerMethodName;

    public static object Stub()
    {
        throw new UnreachableException("Method intended to be reverse patched");
    }
}
