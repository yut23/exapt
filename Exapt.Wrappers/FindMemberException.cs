namespace Exapt.Wrappers;

public class FindMemberException : Exception
{
    public FindMemberException() { }

    public FindMemberException(string message)
        : base(message) { }

    public FindMemberException(string message, Exception innerException)
        : base(message, innerException) { }
}
