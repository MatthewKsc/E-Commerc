using API.Dtos;
using API.Exceptions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController: ControllerBase
    {

        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper) {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreatOrder([FromBody] OrderDTO orderDTO) {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var address = mapper.Map<Address>(orderDTO.shipToAddress);

            var order = await orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, address);

            if (order == null)
                return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult> GetOrdersForUser() {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var orders = await orderService.GetOrdersForUserAsync(email);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById([FromRoute] int id) {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var order = await orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(order);

        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult> GetDDeliveryMethods() {
            return Ok(await orderService.GetDeliveryMethodsAsync());
        }
    }
}
