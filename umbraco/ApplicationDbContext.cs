namespace umbraco;
using Microsoft.EntityFrameworkCore;

using umbraco.Models;

using Umbraco.Cms.Core.Models.Email;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<EmailLog> EmailLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailLog>()
            .HasKey(e => e.EmailId);
    }
}
