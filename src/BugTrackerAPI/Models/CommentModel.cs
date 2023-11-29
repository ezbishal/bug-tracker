using BugTrackerAPI.Models.Bugs;

namespace BugTrackerAPI.Models.Comments;

public class CommentModel
{
    public int Id { get; set; }
    public string Details { get; set; }
    public DateTime CreatedDate { get; set; }
    public int BugId { get; set; }
    public BugModel Bug { get; set; }
}
