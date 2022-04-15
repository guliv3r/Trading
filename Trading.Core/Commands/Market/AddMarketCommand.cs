using AutoMapper;
using MediatR;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Commands.Market
{
    public class AddMarketCommand : AppRequest
    {
        public AddMarketCommand(MarketDto market)
        {
            Market = market;
        }

        public MarketDto Market { get; set; }
    }

    internal class AddMarketCommandHandler : IRequestHandler<AddMarketCommand, Response>
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;

        public AddMarketCommandHandler(IMarketRepository marketRepository, IMapper mapper)
        {
            _marketRepository = marketRepository is IMarketRepository ? marketRepository : throw new ArgumentNullException(nameof(marketRepository));
            _mapper = mapper is IMapper ? mapper : throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(AddMarketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.Add(_mapper.Map<Infrastructure.Database.Models.Market>(request.Market));
                return new Response.Successed<MarketDto>(_mapper.Map<MarketDto>(res));
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_ADD_Market", ex.Message);
            }
        }
    }
}
