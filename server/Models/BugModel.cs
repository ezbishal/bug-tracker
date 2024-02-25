using Server.Authentication;
using Server.Enums;

namespace Server.Models;

public class BugModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public BugStatusEnum Status { get; set; }
    public int ProjectId { get; set; }
    public BugSeverityEnum Severity { get; set; }
    public DateTime DateReported { get; set; }
    public DateTime? DateResolved { get; set; }
    public string ReproductionSteps { get; set; }
    public List<string> Attachments { get; set; }

}