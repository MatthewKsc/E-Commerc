using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class ResponseCasheService : IResponseCasheService {

        private readonly IDatabase database;

        public ResponseCasheService(IConnectionMultiplexer redis) {
            database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string casheKey, object response, TimeSpan timeToLive) {
            if(response == null) {
                return;
            }

            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serialisedResponse = JsonSerializer.Serialize(response, options);

            await database.StringSetAsync(casheKey, serialisedResponse, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string casheKey) {
            var cachedResponse = await database.StringGetAsync(casheKey);

            if (cachedResponse.IsNullOrEmpty) {
                return null;
            }

            return cachedResponse;
        }
    }
}
