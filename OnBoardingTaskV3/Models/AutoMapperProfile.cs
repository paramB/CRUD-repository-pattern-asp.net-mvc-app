using AutoMapper;
using System.Collections;
using System.Linq;

namespace OnBoardingTaskV3.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductSold, ProductSoldDTO>();
            CreateMap<ProductSoldDTO, ProductSold>();
            
            //CreateMap<ProductSoldDTO, ProductSold>()
            //.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer))
            //.ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product))
            //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            // .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store));
            //.ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId));
        }
    }
}