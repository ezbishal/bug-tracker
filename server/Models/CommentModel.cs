using Server.Authentication;

namespace Server.Models;

public class CommentModel
{
    public int Id { get; set; }
    public int BugId { get; set; }
    public ApplicationUser Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
}