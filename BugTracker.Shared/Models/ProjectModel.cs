using BugTracker.Generator.Attributes;
using BugTracker.Shared.Enums;
using BugTracker.Shared.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Shared.Models;

[GenerateDto("CreateProjectModel", "GetProjectModel", "UpdateProjectModel")]
public class ProjectModel
{
    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]

    public DateTime StartDate { get; set; }
    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]

    public DateTime? EndDate { get; set; }

    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]

    public ICollection<UserModel> TeamMembers { get; set; }
    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]

    public ProjectStatusEnum Status { get; set; }
    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]

    public Uri RepositoryLink { get; set; }

    [ExcludeProperty("CreateProjectModel", "UpdateProjectModel")]

    public ICollection<BugModel> Bugs { get; set; }

}