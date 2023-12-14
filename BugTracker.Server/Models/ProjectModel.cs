namespace BugTracker.Server.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }

    public ICollection<BugModel> Bugs { get; set; }

}