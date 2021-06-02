using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter {

        private readonly int timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds) {
            this.timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCasheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse)) {
                var contentResult = new ContentResult {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if(executedContext.Result is OkObjectResult ok) {
                await cacheService.CacheResponseAsync(cacheKey, cacheResponse, TimeSpan.FromSeconds(timeToLiveSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request) {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key)) {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
