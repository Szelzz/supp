using Supp.Core.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Supp.Web.Infrastructure
{
    public class ShedulerMiddleware
    {
        private readonly IScheduler sheduler;
        private readonly RequestDelegate next;

        public ShedulerMiddleware(RequestDelegate next, IScheduler sheduler)
        {
            this.sheduler = sheduler;
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IEnumerable<ITask> tasks)
        {
            sheduler.ExecuteTasks(tasks);
            await next(httpContext);
        }
    }
}
