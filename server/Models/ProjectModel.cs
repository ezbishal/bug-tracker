namespace Server.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AuthorId { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Uri? RepositoryLink { get; set; }
}