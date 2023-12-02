using BugTrackerApi.Authentication;
using BugTrackerApi.Models.Bugs;
using BugTrackerApi.Models.Comments;
using BugTrackerApi.Models.Projects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerApi.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {

    }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<BugModel> Bugs { get; set; }
    public DbSet<CommentModel> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectModel>().ToTable("course");
        modelBuilder.Entity<BugModel>().ToTable("project");
        modelBuilder.Entity<CommentModel>().ToTable("comment");
        base.OnModelCreating(modelBuilder);

    }
}