using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trading.Core.Models.Response;
using Trading.Core.Queries.Auth;

namespace Trading.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response.Successed<string>), 200)]
        [ProducesResponseType(typeof(Response.Failed), 400)]
        public async Task<Response> Login(string user, string password)
            => await _mediator.Send(new LoginQuery(user, password));
    }
}
