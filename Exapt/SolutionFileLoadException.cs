namespace Exapt;

public class SolutionFileLoadException : Exception
{
    public SolutionFileLoadException() { }

    public SolutionFileLoadException(string? message)
        : base(message) { }

    public SolutionFileLoadException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
