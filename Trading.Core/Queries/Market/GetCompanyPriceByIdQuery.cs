using AutoMapper;
using MediatR;
using Trading.Core.Models;
using Trading.Core.Models.Request;
using Trading.Core.Models.Response;
using Trading.Core.Repositories;

namespace Trading.Core.Query.Market
{
    public class GetCompanyPriceByIdQuery : AppRequest
    {
        public GetCompanyPriceByIdQuery(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; set; }
    }

    internal class GetCompanyPriceByIdQueryHandler : IRequestHandler<GetCompanyPriceByIdQuery, Response>
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;

        public GetCompanyPriceByIdQueryHandler(IMarketRepository marketRepository, IMapper mapper)
        {
            _marketRepository = marketRepository ?? throw new ArgumentNullException(nameof(marketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(GetCompanyPriceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _marketRepository.GetMarketCompanyById(request.CompanyId);
                return new Response.Successed<TradingDto>(_mapper.Map<TradingDto>(res));
            }
            catch (Exception ex)
            {
                return new Response.Failed("ERROR_GET_MARKET_COMPANIES", ex.Message);
            }
        }
    }
}
