using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
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

            CreateMap<Address, AddressDTO>();

            CreateMap<AddressDTO, Address>();

            CreateMap<AppUser, UserDTO>()
                .ForMember(d => d.Email, s => s.MapFrom(appuser => appuser.Email));

            CreateMap<RegisterDTO, AppUser>()
                .ForMember(d => d.Email, s => s.MapFrom(r => r.Email))
                .ForMember(d => d.UserName, s => s.MapFrom(r => r.Email));
        }
    }
}
