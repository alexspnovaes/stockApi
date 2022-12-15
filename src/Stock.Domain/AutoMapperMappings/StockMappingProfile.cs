using AutoMapper;
using Stock.Domain.Models;

namespace Stock.Domain.AutoMapperMappings
{
    public class StockMappingProfile : Profile
    {
        public StockMappingProfile()
        {
            CreateMap<Models.Stock, StockModel>().ReverseMap();
        }
    }
}
