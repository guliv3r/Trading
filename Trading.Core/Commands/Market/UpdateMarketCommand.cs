using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Market
{
    public class UpdateMarketCommand : AppRequest
    {
        public UpdateMarketCommand(MarketDto market)
        {
            Market = market;
        }

        public MarketDto Market { get; set; }
    }

    internal class UpdateMarketCommandHandler : IRequestHandler<UpdateMarketCommand, Response>
    {
        private readonly IMarketRepository _marketRepository;

        public UpdateMarketCommandHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository is IMarketRepository ? marketRepository : throw new ArgumentNullException(nameof(marketRepository));
        }

        public async Task<Response> Handle(UpdateMarketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.Update(request.Market.ToEntity());
                return new Response.Successed<MarketDto>(res.ToDomain());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_UPDATE_MARKET", ex.Message);
            }
        }
    }
}
