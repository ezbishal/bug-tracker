using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Authentication;
using Server.Models;

namespace Server.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<BugModel> Projects { get; set; }
    public DbSet<BugModel> Bugs { get; set; }
    public DbSet<CommentModel> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BugModel>().ToTable("bug");
        modelBuilder.Entity<BugModel>().ToTable("project");
        modelBuilder.Entity<CommentModel>().ToTable("comment");
        base.OnModelCreating(modelBuilder);

    }
}