using MediatR;
using Trading.Core.Mapper;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Query.Market
{
    public class GetMarketByIdQuery : AppRequest
    {
        public GetMarketByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    internal class GetMarketByIdQueryHandler : IRequestHandler<GetMarketByIdQuery, Response>
    {
        private readonly IMarketRepository _marketRepository;

        public GetMarketByIdQueryHandler(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository ?? throw new ArgumentNullException(nameof(marketRepository));
        }

        public async Task<Response> Handle(GetMarketByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.GetById(request.Id);
                return new Response.Successed<MarketDto>(res.ToDomain());
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_MARKET", ex.Message);
            }
        }
    }
}
