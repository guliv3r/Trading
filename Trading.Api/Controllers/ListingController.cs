using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trading.Core.Commands.Listing;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;

namespace Trading.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ListingController : ControllerBase
    {

        private readonly ILogger<ListingController> _logger;
        private readonly IMediator _mediator;

        public ListingController(ILogger<ListingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator is IMediator ? mediator : throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response.Successed<MarketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response.Failed), StatusCodes.Status400BadRequest)]
        public async Task<Response> ChangePrice([FromBody] ChangePriceRequest changePriceRequest)
            => await _mediator.Send(new ChangePriceCommand(changePriceRequest));
    }
}