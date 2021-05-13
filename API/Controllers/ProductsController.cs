using API.Dtos;
using API.Exceptions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<ActionResult> GetProducts([FromQuery] string sort ,
            [FromQuery]int? brandId,
            [FromQuery]int? typeId) {

            var spec = new ProductWithTypesAndBrandSpecification(sort, brandId, typeId);

            var products = await productsRepo.GetListWithSpec(spec);

            var result = mapper.Map<IReadOnlyList<ProductDTO>>(products);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProduct([FromRoute] int id) {

            var spec = new ProductWithTypesAndBrandSpecification(id);

            var product = await productsRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

            var result = mapper.Map<ProductDTO>(product);

            return Ok(result);
        }

    }
}
