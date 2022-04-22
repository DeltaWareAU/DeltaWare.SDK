using System;

namespace DeltaWare.SDK.Benchmarking.Results
{
    internal class MetricResult : IMetricResult, IMetricTracker
    {
        public string Name { get; }

        public string Description { get; }

        public long TotalTicks { get; protected set; }

        public long Iterations { get; protected set; }

        public long MaximumTicks { get; protected set; }

        public long MinimumTicks { get; protected set; } = long.MaxValue;

        public decimal AverageTicks => Math.Round((decimal)TotalTicks / Iterations, 2);

        public MetricResult(string name, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }

        public virtual void Update(long ticks)
        {
            Iterations++;
            TotalTicks += ticks;

            if (ticks > MaximumTicks)
            {
                MaximumTicks = ticks;
            }

            if (ticks < MaximumTicks)
            {
                MinimumTicks = ticks;
            }
        }
    }
}
