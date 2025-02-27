using AutoMapper;
using GlassLewisAssessment.application.DTOs;
using GlassLewisAssessment.domain.Interfaces;
using MediatR;

namespace GlassLewisAssessment.application.Queries.Handlers
{
    public class GetCompanyByIsinQueryHandler : IRequestHandler<GetCompanyByIsinQuery, CompanyDTO?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetCompanyByIsinQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDTO?> Handle(GetCompanyByIsinQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CompanyDTO?>(await _companyRepository.GetByIsinAsync(request.Isin));
        }
    }
}