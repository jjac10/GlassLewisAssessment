using GlassLewisAssessment.domain.Entities;
using GlassLewisAssessment.domain.Interfaces;
using GlassLewisAssessment.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisAssessment.infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IApplicationDbContext _context;
        public CompanyRepository(IApplicationDbContext context) => _context = context;

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companies.AsNoTracking().ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company?> GetByIsinAsync(string isin)
        {
            return await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Isin == isin);
        }

        public async Task<int> CreateAsync(Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }

        public async Task<int> UpdateAsync(int id, Company company)
        {
            if (company == null) throw new ArgumentNullException(nameof(company));

            var exist = await GetByIdAsync(id);
            if (exist == null) throw new KeyNotFoundException("Company not found.");

            exist.Name = company.Name;
            exist.Exchange = company.Exchange;
            exist.Ticker = company.Ticker;
            exist.Isin = company.Isin;
            exist.Website = company.Website;

            await _context.SaveChangesAsync();
            return exist.Id;
        }
    }
}