using AutoMapper;
using GlassLewisAssessment.application.DTOs;
using GlassLewisAssessment.domain.Interfaces;
using MediatR;

namespace GlassLewisAssessment.application.Queries.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyDTO>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDTO>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CompanyDTO>>((await _companyRepository.GetAllAsync()).ToList());
        }
    }
}