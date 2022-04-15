using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trading.Core.Commands.Market;
using Trading.Core.Models;
using Trading.Core.Models.Response;
using Trading.Core.Query.Market;

namespace Trading.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MarketController : ControllerBase
    {

        private readonly ILogger<MarketController> _logger;
        private readonly IMediator _mediator;

        public MarketController(ILogger<MarketController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator is IMediator ? mediator : throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response.Successed<MarketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response.Failed), StatusCodes.Status400BadRequest)]
        public async Task<Response> Get(int id)
            => await _mediator.Send(new GetMarketByIdQuery(id));

        [HttpGet]
        [ProducesResponseType(typeof(Response.Successed<List<MarketDto>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> GetAll()
            => await _mediator.Send(new GetMarketQuery());

        [HttpPost]
        [ProducesResponseType(typeof(Response.Successed<List<MarketDto>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Add([FromBody] MarketDto market)
            => await _mediator.Send(new AddMarketCommand(market));

        [HttpPut]
        [ProducesResponseType(typeof(Response.Successed<List<bool>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Update([FromBody] MarketDto market)
            => await _mediator.Send(new UpdateMarketCommand(market));

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response.Successed<List<bool>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Remove(int id)
            => await _mediator.Send(new RemoveMarketCommand(id));

        [HttpGet("{marketId}")]
        [ProducesResponseType(typeof(Response.Successed<List<bool>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> GetMarketCompanies(int marketId)
            => await _mediator.Send(new GetMarketCompaniesQuery(marketId));

        [HttpGet("{companyId}")]
        [ProducesResponseType(typeof(Response.Successed<List<bool>>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> GetMarketCompaniesByCompanyId(int companyId)
            => await _mediator.Send(new GetCompanyPriceByIdQuery(companyId));
    }
}