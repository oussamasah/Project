using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Identity.Data;
using Project.Models;


namespace Project.Data;

public class AuhtDbContext : IdentityDbContext<AppplicationUser>
{
    public AuhtDbContext()
    {
    }

    public AuhtDbContext(DbContextOptions<AuhtDbContext> options)
        : base(options)
    {
    }
    public DbSet<Tasks> task { get; set; }
    public DbSet<Projects> project { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
