using BugTrackerAPI.Helpers;

namespace BugTrackerAPI.Exceptions;

public class InputValidationException : Exception
{
    public ErrorDetailsCollection ErrorCollection { get; }

    public InputValidationException(ErrorDetailsCollection errorCollection)
    {
        ErrorCollection = errorCollection;
    }
}
