using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Authentication;
using Server.Models;

namespace Server.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<BugModel> Bugs { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
    public DbSet<ApiKeyModel> ApiKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BugModel>().ToTable("bug");
        builder.Entity<ProjectModel>().ToTable("project");
        builder.Entity<CommentModel>().ToTable("comment");
        base.OnModelCreating(builder);

    }
}
