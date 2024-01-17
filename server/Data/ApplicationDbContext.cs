using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Authentication;
using server.Models;

namespace server.Data;

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
        modelBuilder.Entity<ProjectModel>().ToTable("bug");
        modelBuilder.Entity<BugModel>().ToTable("project");
        modelBuilder.Entity<CommentModel>().ToTable("comment");
        base.OnModelCreating(modelBuilder);

    }
}