using BugTrackerAPI.Helpers;

namespace BugTrackerAPI.Exceptions;

public class DomainException : Exception
{
    public ErrorDetailsCollection ErrorCollection { get; }

    public DomainException(ErrorDetailsCollection errorCollection)
    {
        ErrorCollection = errorCollection;
    }
}
