using BugTrackerApi.Models.Comments;
using BugTrackerApi.Models.Projects;

namespace BugTrackerApi.Models.Bugs;

public class BugModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }

    public ICollection<CommentModel> Comments { get; set; }
}
