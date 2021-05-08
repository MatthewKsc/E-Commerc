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
        
        private readonly IProductRepository repo;

        public ProductsController(IProductRepository repo) {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts() {

            var result = await repo.GetProductsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromRoute] int id) {

            var result = await repo.GetProductByIdAsync(id);

            return Ok(result);
        }

    }
}
