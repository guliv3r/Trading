using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Query.Market
{
    public class GetMarketQuery : AppRequest
    {
    }

    internal class GetMarketQueryHandler : IRequestHandler<GetMarketQuery, Response>
    {
        private readonly IMarketRepository _marketRepository;

        public GetMarketQueryHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository ?? throw new ArgumentNullException(nameof(marketRepository));
        }

        public async Task<Response> Handle(GetMarketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.Get();
                return new Response.Successed<List<MarketDto>>(res.Select(x => x.ToDomain()).ToList());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_MARKETS", ex.Message);
            }
        }
    }
}
