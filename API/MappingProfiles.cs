using API.Dtos;
using AutoMapper;
using Core.Entities;
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
        }
    }
}
