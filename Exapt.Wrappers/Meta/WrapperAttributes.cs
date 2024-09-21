namespace Exapt.Wrappers.Meta;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ClassWrapperAttribute(string innerClassName) : Attribute
{
    public string InnerClassName { get; private set; } = innerClassName;
}
