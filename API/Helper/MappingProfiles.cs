using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

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

            CreateMap<Core.Entities.Identity.Address,AddressDto>().ReverseMap();
            
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();

            // full path
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            
            // d => destination
            // o => options
            // s => source
            CreateMap<Order,OrderToReturnDto>()
                .ForMember(
                    d => d.DeliveryMethod,
                    o => o.MapFrom(
                        s => s.DeliveryMethod.ShortName)
                )
                .ForMember(
                    d => d.ShippingPrice,
                    o => o.MapFrom(
                        s => s.DeliveryMethod.Price)
                );

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(
                    d => d.ProductId,
                    o => o.MapFrom(
                        s => s.ItemOrdered.ProductItemId
                    )
                )
                .ForMember(
                    d => d.ProductName,
                    o => o.MapFrom(
                        s => s.ItemOrdered.ProductName
                    )
                )
                .ForMember(
                    d => d.PictureUrl,
                    o => o.MapFrom(
                        s => s.ItemOrdered.PictureUrl
                    )
                )
                .ForMember(
                    d => d.PictureUrl,
                    o => o.MapFrom<OrderItemUrlResolver>()
                );
        }
    }
}