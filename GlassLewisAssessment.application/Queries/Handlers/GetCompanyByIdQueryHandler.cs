using AutoMapper;
using GlassLewisAssessment.application.DTOs;
using GlassLewisAssessment.domain.Interfaces;
using MediatR;

namespace GlassLewisAssessment.application.Queries.Handlers
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDTO?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDTO?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CompanyDTO?>(await _companyRepository.GetByIdAsync(request.Id));
        }
    }
}