using API.dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        
        private readonly StoreContext context;

        public ProductsController(StoreContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts() {

            var result = await context.Products.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromRoute] int id) {

            var result = await context.Products
                .SingleOrDefaultAsync(p => p.Id == id);

            return Ok(result);
        }

    }
}
