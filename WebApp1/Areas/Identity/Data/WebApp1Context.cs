using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp1.Areas.Identity.Data;
using WebApp1.Models;

namespace WebApp1.Data;

public class WebApp1Context : IdentityDbContext<WebApp1User>
{
    public WebApp1Context(DbContextOptions<WebApp1Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<WebApp1.Models.Friend> WebApp1User { get; set; }
    public DbSet<WebApp1.Models.Friend> Friend { get; set; }

    public DbSet<WebApp1.Models.Note> Note { get; set; }

    public DbSet<WebApp1.Models.Message> Message { get; set; }
}
