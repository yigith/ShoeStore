using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static void UseBasketTransfer(this IApplicationBuilder app)
        {
            app.UseMiddleware<BasketTransferMiddleware>();
        }
    }
}
