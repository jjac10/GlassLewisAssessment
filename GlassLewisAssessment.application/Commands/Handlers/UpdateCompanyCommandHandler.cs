using AutoMapper;
using GlassLewisAssessment.domain.Entities;
using GlassLewisAssessment.domain.Interfaces;
using MediatR;

namespace GlassLewisAssessment.application.Commands.Handlers
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommnad, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCompanyCommnad request, CancellationToken cancellationToken)
        {
            return await _companyRepository.UpdateAsync(request.Id, _mapper.Map<Company>(request.Company));
        }
    }
}