using System;
using System.Collections.Generic;
using System.Linq;

namespace DeltaWare.SDK.Benchmarking.Results
{
    internal class MetricResult : IBenchmarkResult
    {
        public string Name { get; }

        public string Description { get; }

        public long LastTicks { get; set; }

        public long TotalTicks { get; protected set; }

        public long Iterations { get; protected set; }

        public long MaximumTicks { get; protected set; }

        public long MinimumTicks { get; protected set; } = long.MaxValue;

        public decimal AverageTicks => Math.Round((decimal)TotalTicks / Iterations, 2);

        protected List<MetricResult> ChildResults { get; } = new List<MetricResult>();

        public IReadOnlyList<IBenchmarkResult> Results => ChildResults;

        public MetricResult CreateChild(string name, string description = null)
        {
            MetricResult result = new MetricResult(name, description);

            ChildResults.Add(result);

            return result;
        }

        public MetricResult(string name, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
        }

        public virtual void Update(long ticks)
        {
            LastTicks = ticks;
        }

        protected void Update()
        {
            foreach (MetricResult childResult in ChildResults)
            {
                childResult.Update();
            }

            LastTicks += ChildResults.Sum(r => r.LastTicks);

            Iterations++;
            TotalTicks += LastTicks;

            if (LastTicks > MaximumTicks)
            {
                MaximumTicks = LastTicks;
            }

            if (LastTicks < MaximumTicks)
            {
                MinimumTicks = LastTicks;
            }
        }
    }
}
