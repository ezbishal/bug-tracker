using BugTracker.Generator.Attributes;
using BugTracker.Shared.Enums;
using BugTracker.Shared.UserModels;
using System;
using System.Collections.Generic;

namespace BugTracker.Shared.Models;

[GenerateDto("CreateBugModel", "GetBugModel", "UpdateBugModel")]
public class BugModel
{
    [ExcludeProperty("CreateBugModel", "UpdateBugModel")]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public UserModel ReportedBy { get; set; }
    public UserModel AssignedTo { get; set; }
    public BugStatusEnum Status { get; set; }
    public int ProjectId { get; set; }
    public BugSeverityEnum Severity { get; set; }
    public DateTime DateReported { get; set; }
    public DateTime? DateResolved { get; set; }
    public string ReproductionSteps { get; set; }
    public List<string> Attachments { get; set; }

}