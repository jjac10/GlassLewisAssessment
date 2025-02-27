using GlassLewisAssessment.application.Commands;
using GlassLewisAssessment.application.DTOs;
using GlassLewisAssessment.application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlassLewisAssessment.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllCompaniesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _mediator.Send(new GetCompanyByIdQuery(id)));

        [HttpGet("isin/{isin}")]
        public async Task<IActionResult> GetByIsin(string isin) => Ok(await _mediator.Send(new GetCompanyByIsinQuery(isin)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDTO company) => Ok(await _mediator.Send(new CreateCompanyCommand(company)));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyDTO company) => Ok(await _mediator.Send(new UpdateCompanyCommand(id, company)));
    }
}