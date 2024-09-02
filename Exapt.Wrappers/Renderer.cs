namespace Exapt.Wrappers;

public class Renderer : Meta.StaticWrapper<Renderer>
{
    static Renderer()
    {
        SetWrappedType("Renderer");
    }

    public static void Initialize(RendererType type, bool debugMode)
    {
        _ = CallStatic("#=qjnHbEJCc8TIMoikd2FebJA==", type, debugMode);
    }
}
