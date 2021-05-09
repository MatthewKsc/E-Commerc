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
    public class TypesController : ControllerBase
    {
        private readonly IGenericRepository<ProductType> typeRepo;

        public TypesController(IGenericRepository<ProductType> typeRepo) {
            this.typeRepo = typeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes() {
            var result = await typeRepo.GetListAsync();

            return Ok(result);
        }
    }
}
