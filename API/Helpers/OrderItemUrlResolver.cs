using API.Dtos;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string> {

        private readonly IConfiguration config;

        public OrderItemUrlResolver(IConfiguration config) {
            this.config = config;
        }

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context) {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureURL)) {
                return config["ApiUrl"] + source.ItemOrdered.PictureURL;
            }

            return null;
        }
    }
}
