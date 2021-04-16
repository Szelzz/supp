using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Supp.Core.Scheduler
{
    public class InMemoryScheduler : IScheduler
    {
        private readonly ILogger<InMemoryScheduler> logger;
        private static readonly Dictionary<string, DateTime> executed = new Dictionary<string, DateTime>();
        private static readonly object shedulerLock = new object();

        public InMemoryScheduler(ILogger<InMemoryScheduler> logger)
        {
            this.logger = logger;
        }

        public void ExecuteTasks(IEnumerable<ITask> tasks)
        {
            if (!Monitor.TryEnter(shedulerLock))
                return;

            try
            {
                foreach (var task in tasks)
                {
                    if (executed.TryGetValue(task.Id, out DateTime lastTimeExecuted))
                    {
                        if (DateTime.Now - task.Interval > lastTimeExecuted)
                            continue;
                    }
                    try
                    {
                        task.Execute();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error while executing task: " + task.Id);
                    }
                    executed[task.Id] = DateTime.Now;
                }
            }
            finally
            {
                Monitor.Exit(shedulerLock);
            }
        }
    }
}
