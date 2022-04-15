using AutoMapper;
using Trading.Core.Models;
using Trading.Infrastructure.Database.Models;

namespace Trading.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<MarketDto, Market>().ReverseMap();
            CreateMap<TradingDto, Infrastructure.Database.Models.Trading>().ReverseMap();
        }
    }
}