namespace BugTracker.Shared.Enums;

public enum ProjectStatusEnum
{
	NotStarted,  // The project is created but work has not commenced
	Active,      // The project is in progress
	OnHold,      // Work on the project is temporarily paused
	Completed,   // The project is finished
	Cancelled    // The project is abandoned or terminated before completion
}
