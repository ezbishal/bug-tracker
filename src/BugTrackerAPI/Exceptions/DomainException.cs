using BugTrackerApi.Helpers;

namespace BugTrackerApi.Exceptions;

public class DomainException : Exception
{
    public ErrorDetailsCollection ErrorCollection { get; }

    public DomainException(ErrorDetailsCollection errorCollection)
    {
        ErrorCollection = errorCollection;
    }
}
