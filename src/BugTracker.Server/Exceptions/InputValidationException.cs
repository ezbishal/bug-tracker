namespace BugTrackerApi.Exceptions;

public class InputValidationException : Exception
{
    public ErrorDetailsCollection ErrorCollection { get; }

    public InputValidationException(ErrorDetailsCollection errorCollection)
    {
        ErrorCollection = errorCollection;
    }
}
