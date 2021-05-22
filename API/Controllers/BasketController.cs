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
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository) {
            this.basketRepository = basketRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetBasketById([FromQuery] string id) {
            var basket = await basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasekt(CustomerBasket basket) {
            var updatedBasket = await basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasekt([FromQuery] string id) {
            await basketRepository.DeleteBasketAsync(id);
            
            return NoContent();
        }
    }
}
