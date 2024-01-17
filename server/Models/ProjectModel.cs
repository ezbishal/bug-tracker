using server.Authentication;
using server.Enums;

namespace server.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<ApplicationUser> TeamMembers { get; set; }
    public ProjectStatusEnum Status { get; set; }
    public Uri RepositoryLink { get; set; }
    public ICollection<BugModel> Bugs { get; set; }

}