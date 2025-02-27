using GlassLewisAssessment.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisAssessment.infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Company> Companies { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=CompanyDB.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Isin)
                .HasDatabaseName("IDX_Company_Isin")
                .IsUnique();
        }
    }
}