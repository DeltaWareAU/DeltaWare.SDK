using System;
using System.Diagnostics;
using DeltaWare.SDK.Benchmarking.Metrics.Builder;
using DeltaWare.SDK.Benchmarking.Results;

namespace DeltaWare.SDK.Benchmarking.Metrics
{
    public class Metric: IMetricMeasure
    {
        public string Name { get; }

        public string Description { get; }

        private long _executionTicks = 0;

        private readonly MetricResult _metricResult;

        public IMetricResult Result => _metricResult;

        public Metric(string name, string description = null)
        {
            Name = name;
            Description = description;

            _metricResult = new MetricResult(name, description);
        }

        public void Measure(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            action.Invoke();
            
            stopwatch.Stop();

            _executionTicks = stopwatch.ElapsedTicks;

            _metricResult.Update(_executionTicks);
        }

        internal long GetExecutionTime()
        {
            return _executionTicks;
        }
    }
}
