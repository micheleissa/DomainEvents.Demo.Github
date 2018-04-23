using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DomainEvents.Demo
{
    public class DomainEventsMiddleware
    {
        private readonly RequestDelegate _next;

        public DomainEventsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //capture dependency container for request
            DomainEvents.ServiceProvider = context.RequestServices;

            //continue calling pipeline
            await _next(context);

            //HTTP request is over,
            //remove reference to dependency container
            DomainEvents.ServiceProvider = null;
        }
    }

    public static class DomainEventsEx
    {
        public static IApplicationBuilder UseDomainEvents(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DomainEventsMiddleware>();
        }
    }
}
