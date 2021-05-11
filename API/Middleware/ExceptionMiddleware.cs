using API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env) {
            this.next = next;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            }catch(Exception e) {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var response = env.IsDevelopment() ? new ApiException(statusCode, e.Message, e.StackTrace.ToString())
                    : new ApiException(statusCode);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

    }
}
