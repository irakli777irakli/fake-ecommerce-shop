using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {   
            // Product => ProductToReturnDto
            CreateMap<Product, ProductToReturnDto>()
            //affects ProductToReturnDto destination
                .ForMember(
                d => d.ProductBrand, // map here
                o => o.MapFrom(s => s.ProductBrand.Name)) // from here this
                .ForMember(
                    d => d.ProductType,
                    o => o.MapFrom(s => s.ProductType.Name)
                )
                .ForMember(
                    d => d.PictureUrl,
                    o => o.MapFrom<ProductUrlResolver>()
                );
        }
    }
}