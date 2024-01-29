
    enum ProjectStatusEnum
    {
        NotStarted = "Not Started", // The project is created but work has not commenced
        Active = "Active", // The project is in progress
        OnHold = "On Hold", // Work on the project is temporarily paused   
        Completed = "Completed", // The project is finished
        Cancelled = "Cancelled" // The project is abandoned or terminated before completion
    }

    export default ProjectStatusEnum;