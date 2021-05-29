using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string> {

        private readonly IConfiguration config;

        public ProductUrlResolver(IConfiguration config) {
            this.config = config;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context) {

            if (!string.IsNullOrEmpty(source.PictureURL)) {
                return config["ApiUrl"] + source.PictureURL;
            }

            return null;
        }
    }
}
