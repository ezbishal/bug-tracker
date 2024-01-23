namespace Server.Enums;

public enum BugStatusEnum
{
    Open,         // When the bug is first reported and yet to be addressed
    InProgress,   // When work has started on the bug
    Fixed,        // When the bug has been fixed but not yet released
    Resolved,     // When the bug fix has been verified
    Closed,       // When the bug is closed, either fixed or deemed not a bug
    OnHold,       // When work on the bug is paused (e.g., awaiting more info)
    Reopened      // If a closed/resolved bug is found to still be an issue
}