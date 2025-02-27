using AutoMapper;
using GlassLewisAssessment.domain.Entities;
using GlassLewisAssessment.domain.Interfaces;
using MediatR;

namespace GlassLewisAssessment.application.Commands.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyRepository.CreateAsync(_mapper.Map<Company>(request.Company));
        }
    }
}