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
    public class BrandsController : ControllerBase
    {
        private readonly IGenericRepository<ProductBrand> brandsRepo;

        public BrandsController(IGenericRepository<ProductBrand> brandsRepo) {
            this.brandsRepo = brandsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands() {

            var spec = new BaseSpecification<ProductBrand>();

            var result = await brandsRepo.GetListWithSpec(spec);

            return Ok(result);
        }
    }
}
