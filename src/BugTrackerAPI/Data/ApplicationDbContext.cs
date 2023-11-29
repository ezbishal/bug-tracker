using BugTrackerApi.Models.Bugs;
using BugTrackerApi.Models.Comments;
using BugTrackerApi.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerApi.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<BugModel> Bugs { get; set; }
    public DbSet<CommentModel> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectModel>().ToTable("course");
        modelBuilder.Entity<BugModel>().ToTable("project");
        modelBuilder.Entity<CommentModel>().ToTable("comment");
    }
}