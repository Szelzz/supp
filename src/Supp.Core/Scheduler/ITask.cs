using System;

namespace Supp.Core.Scheduler
{
    public interface ITask
    {
        TimeSpan Interval { get; }
        string Id { get; }

        void Execute();
    }
}
