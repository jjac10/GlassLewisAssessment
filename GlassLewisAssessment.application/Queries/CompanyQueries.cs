using GlassLewisAssessment.application.DTOs;
using MediatR;

namespace GlassLewisAssessment.application.Queries
{
    public record GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDTO>>;
    public record GetCompanyByIdQuery(int Id) : IRequest<CompanyDTO?>;
    public record GetCompanyByIsinQuery(string Isin) : IRequest<CompanyDTO?>;
}