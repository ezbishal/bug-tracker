using BugTracker.Shared.Generator.Attributes;
using BugTracker.Shared.UserModels;
using System;

namespace BugTracker.Shared.Models;

[GenerateDto("CreateCommentModel", "GetCommentModel", "UpdateCommentModel")]
public class CommentModel
{
    [ExcludeProperty("CreateCommentModel", "UpdateCommentModel")]
    public int Id { get; set; }
    public int BugId { get; set; }
    public UserModel Author { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
}