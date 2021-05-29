using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.ProductBrand, s => s.MapFrom(p => p.ProductBrand.Name))
                .ForMember(d => d.ProductType, s => s.MapFrom(p => p.ProductType.Name))
                .ForMember(d => d.PictureURL, s => s.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDTO>();
            CreateMap<AddressDTO, Core.Entities.Identity.Address>();

            CreateMap<AddressDTO, Core.Entities.OrderAggregate.Address>();

            CreateMap<AppUser, UserDTO>()
                .ForMember(d => d.Email, s => s.MapFrom(appuser => appuser.Email));

            CreateMap<CustomerBasketDTO, CustomerBasket>();

            CreateMap<BasketItemDTO, BaskeItem>();

            CreateMap<RegisterDTO, AppUser>()
                .ForMember(d => d.Email, s => s.MapFrom(r => r.Email))
                .ForMember(d => d.UserName, s => s.MapFrom(r => r.Email));

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.DeliveryMethod, s=> s.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, s=> s.MapFrom(o => o.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, s => s.MapFrom(o => o.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProdcutName, s => s.MapFrom(o => o.ItemOrdered.ProductName))
                .ForMember(d => d.PictureURL, s => s.MapFrom(o => o.ItemOrdered.PictureURL))
                .ForMember(d => d.PictureURL, s => s.MapFrom<OrderItemUrlResolver>());
        }
    }
}
