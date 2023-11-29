using BugTrackerAPI.Models.Bugs;

namespace BugTrackerAPI.Models.Projects;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }

    public ICollection<BugModel> Bugs { get; set; }

}