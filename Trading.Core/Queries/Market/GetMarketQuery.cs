using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;

        public GetMarketQueryHandler(IMarketRepository marketRepository, IMapper mapper)
        {
            _marketRepository = marketRepository ?? throw new ArgumentNullException(nameof(marketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(GetMarketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.Get();
                return new Response.Successed<List<MarketDto>>(_mapper.Map<List<MarketDto>>(res));
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_MARKETS", ex.Message);
            }
        }
    }
}
