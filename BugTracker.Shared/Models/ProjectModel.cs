using BugTracker.Shared.Enums;
using BugTracker.Shared.Generator.Attributes;
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
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ICollection<UserModel> TeamMembers { get; set; }
    public ProjectStatusEnum Status { get; set; }
    public Uri RepositoryLink { get; set; }

    public ICollection<BugModel> Bugs { get; set; }

    public int BugCount
    {
        get => Bugs is not null ? Bugs.Count() : 0;
        set => BugCount = value;
    }

}