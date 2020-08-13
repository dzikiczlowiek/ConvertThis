using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace ConvertThis.WebApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
