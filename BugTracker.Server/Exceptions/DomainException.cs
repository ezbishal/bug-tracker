namespace BugTracker.Server.Exceptions;

public class DomainException : Exception
{
    public ErrorDetailsCollection ErrorCollection { get; }

    public DomainException(ErrorDetailsCollection errorCollection)
    {
        ErrorCollection = errorCollection;
    }
}
