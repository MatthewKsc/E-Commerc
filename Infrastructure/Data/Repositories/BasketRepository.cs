using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data.Repositories
{
    public class BasketRepository : IBasketRepository {

        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer redis) {
            database = redis.GetDatabase();
        }
        
        public async Task<CustomerBasket> GetBasketAsync(string basketId) {
            var data = await database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket) {
            var created = await database.StringSetAsync(basket.Id,
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string basketId) {
            return await database.KeyDeleteAsync(basketId);
        }
    }
}
