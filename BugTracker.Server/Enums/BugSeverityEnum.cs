namespace BugTracker.Shared.Enums;

public enum BugSeverityEnum
{
	Critical, // Indicates a system crash or a loss of functionality
	High,     // Major functionality is impacted and there's no workaround
	Medium,   // A workaround is possible but not ideal
	Low,      // Minor problem or an aesthetic issue, not affecting functionality
	Trivial   // Very minor issue, like a typo or a slight UI misalignment
}