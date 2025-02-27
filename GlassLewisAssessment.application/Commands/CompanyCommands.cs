using GlassLewisAssessment.application.DTOs;
using MediatR;

namespace GlassLewisAssessment.application.Commands
{
    public record CreateCompanyCommand(CompanyDTO Company) : IRequest<int>;
    public record UpdateCompanyCommand(int Id, CompanyDTO Company) : IRequest<int>;
}