using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class OrderService : IOrderService {

        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepo;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo) {
            this.unitOfWork = unitOfWork;
            this.basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmial, int deliveryMethodId, string basketId, Address ShippingAddress) {
            var basket = await basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();

            foreach(var item in basket.Items) {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, buyerEmial, ShippingAddress, deliveryMethod, subtotal);

            unitOfWork.Repository<Order>().Add(order);
            var result = await unitOfWork.Complete();

            if (result <= 0) return null;

            await basketRepo.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync() {
            return await unitOfWork.Repository<DeliveryMethod>().GetListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail) {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail) {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await unitOfWork.Repository<Order>().GetListWithSpec(spec);
        }
    }
}
