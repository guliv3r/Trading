using MediatR;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Market
{
    public class RemoveMarketCommand : AppRequest
    {
        public RemoveMarketCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    internal class RemoveMarketCommandHandler : IRequestHandler<RemoveMarketCommand, Response>
    {
        private readonly IMarketRepository _marketRepository;

        public RemoveMarketCommandHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository is IMarketRepository ? marketRepository : throw new ArgumentNullException(nameof(marketRepository));
        }

        public async Task<Response> Handle(RemoveMarketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.Remove(request.Id);
                return new Response.Successed<bool>(true);
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_REMOVE_MARKET", ex.Message);
            }
        }
    }
}
