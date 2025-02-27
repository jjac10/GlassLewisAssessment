using GlassLewisAssessment.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisAssessment.infrastructure.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}