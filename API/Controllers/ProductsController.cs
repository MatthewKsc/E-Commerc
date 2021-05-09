using Core.Entities;
using Core.Interfaces;
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
        
        private readonly IGenericRepository<Product> productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo) {
            this.productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts() {

            var result = await productsRepo.GetListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromRoute] int id) {

            var result = await productsRepo.GetByIdAsync(id);

            return Ok(result);
        }

    }
}
