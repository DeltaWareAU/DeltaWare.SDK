using DeltaWare.SDK.Benchmarking.Results;
using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Benchmarking.Metrics
{
    internal class Metric : IMetric
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public MetricResult Result { get; set; }

        public Metric()
        {

        }

        public Metric(string name, string description = null)
        {
            Name = name;
            Description = description;


        }

        public void Measure(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            action.Invoke();

            stopwatch.Stop();

            Result.LastTicks = stopwatch.ElapsedTicks;
        }
    }
}
