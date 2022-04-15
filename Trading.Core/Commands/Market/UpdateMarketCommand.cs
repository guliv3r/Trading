using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;

        public UpdateMarketCommandHandler(IMarketRepository marketRepository, IMapper mapper)
        {
            _marketRepository = marketRepository is IMarketRepository ? marketRepository : throw new ArgumentNullException(nameof(marketRepository));
            _mapper = mapper is IMapper ? mapper : throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(UpdateMarketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.Update(_mapper.Map<Infrastructure.Database.Models.Market>(request.Market));
                return new Response.Successed<MarketDto>(_mapper.Map<MarketDto>(res));
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_UPDATE_MARKET", ex.Message);
            }
        }
    }
}
