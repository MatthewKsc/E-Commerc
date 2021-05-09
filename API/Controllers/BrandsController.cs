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
    public class BrandsController : ControllerBase
    {
        private readonly IGenericRepository<ProductBrand> brandsRepo;

        public BrandsController(IGenericRepository<ProductBrand> brandsRepo) {
            this.brandsRepo = brandsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands() {
            var result = await brandsRepo.GetListAsync();

            return Ok(result);
        }
    }
}
