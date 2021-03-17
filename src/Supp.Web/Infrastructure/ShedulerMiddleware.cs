using Supp.Core.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supp.Core.Scheduler;

namespace Supp.Web.Infrastructure
{
    public class ShedulerMiddleware
    {
        private readonly IScheduler sheduler;

        public ShedulerMiddleware(IScheduler sheduler)
        {
            this.sheduler = sheduler;
        }

        // IMyScopedService is injected into Invoke
        public void Invoke(IEnumerable<ITask> tasks)
        {
            sheduler.ExecuteTasks(tasks);
        }
    }
}
