using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<Product> productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo, IMapper mapper) {
            this.mapper = mapper;
            this.productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts() {

            var spec = new ProductWithTypesAndBrandSpecification();

            var products = await productsRepo.GetListWithSpec(spec);

            var result = mapper.Map<IReadOnlyList<ProductDTO>>(products);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromRoute] int id) {

            var spec = new ProductWithTypesAndBrandSpecification(id);

            var product = await productsRepo.GetEntityWithSpec(spec);

            var result = mapper.Map<ProductDTO>(product);

            return Ok(result);
        }

    }
}
