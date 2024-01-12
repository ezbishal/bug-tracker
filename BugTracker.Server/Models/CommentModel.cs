using BugTracker.Shared.UserModels;

namespace BugTracker.Shared.Models;

public class CommentModel
{
    public int Id { get; set; }
    public int BugId { get; set; }
    public UserModel Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
}