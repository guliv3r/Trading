using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Query.Market
{
    public class GetMarketCompaniesQuery : AppRequest
    {
        public GetMarketCompaniesQuery(int marketId)
        {
            MarketId = marketId;
        }

        public int MarketId { get; set; }
    }

    internal class GetMarketCompaniesQueryHandler : IRequestHandler<GetMarketCompaniesQuery, Response>
    {
        private readonly IMarketRepository _marketRepository;

        public GetMarketCompaniesQueryHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository ?? throw new ArgumentNullException(nameof(marketRepository));
        }

        public async Task<Response> Handle(GetMarketCompaniesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.GetMarketCompanies(request.MarketId);
                return new Response.Successed<List<TradingDto>>(res.Select(x => x.ToDomain()).ToList());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_MARKET_COMPANIES", ex.Message);
            }
        }
    }
}
