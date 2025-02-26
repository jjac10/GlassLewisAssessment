using GlassLewisAssessment.domain.Entities;

namespace GlassLewisAssessment.domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetByIsinAsync(string isin);
        Task<int> CreateAsync(Company company);
        Task<int> UpdateAsync(Company company);
    }
}