using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Benchmarking.Metrics
{
    internal class AsyncMetric : Metric, IAsyncMetric
    {
        public AsyncMetric()
        {
        }

        public async Task Measure(Func<Task> action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            await action.Invoke();

            stopwatch.Stop();

            Result.LastTicks = stopwatch.ElapsedTicks;
        }

        public AsyncMetric(string name, string description = null) : base(name, description)
        {
        }
    }
}
