using API.Helpers;
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
    public class TypesController : ControllerBase
    {
        private readonly IGenericRepository<ProductType> typeRepo;

        public TypesController(IGenericRepository<ProductType> typeRepo) {
            this.typeRepo = typeRepo;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes() {

            var spec = new BaseSpecification<ProductType>();

            var result = await typeRepo.GetListWithSpec(spec);

            return Ok(result);
        }
    }
}
