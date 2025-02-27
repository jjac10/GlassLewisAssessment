using AutoMapper;
using GlassLewisAssessment.application.DTOs;
using GlassLewisAssessment.domain.Entities;

namespace GlassLewisAssessment.application.Mappers
{
    public class CompanyMapperProfile : Profile
    {
        public CompanyMapperProfile() => CreateMap<Company, CompanyDTO>().ReverseMap();
    }
}