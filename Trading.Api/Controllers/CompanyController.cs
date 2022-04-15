using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trading.Core.Commands.Company;
using Trading.Core.Models;
using Trading.Core.Models.Response;
using Trading.Core.Query.Company;

namespace Trading.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IMediator _mediator;

        public CompanyController(ILogger<CompanyController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response.Successed<CompanyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response.Failed), StatusCodes.Status400BadRequest)]
        public async Task<Response> Get(int id)
            => await _mediator.Send(new GetCompanyByIdQuery(id));

        [HttpGet]
        [ProducesResponseType(typeof(Response.Successed<List<CompanyDto>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> GetAll()
            => await _mediator.Send(new GetCompaniesQuery());

        [HttpPost]
        [ProducesResponseType(typeof(Response.Successed<List<MarketDto>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Add([FromBody] CompanyDto company)
            => await _mediator.Send(new AddCompanyCommand(company));

        [HttpPut]
        [ProducesResponseType(typeof(Response.Successed<List<bool>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Update([FromBody] CompanyDto company)
            => await _mediator.Send(new UpdateMarketCommand(company));

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response.Successed<List<bool>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Remove(int id)
            => await _mediator.Send(new RemoveCompanyCommand(id));
    }
}