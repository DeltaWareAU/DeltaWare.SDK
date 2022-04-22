using System.Collections.Generic;
using DeltaWare.SDK.Benchmarking.Metrics;
using DeltaWare.SDK.Benchmarking.Metrics.Builder;
using DeltaWare.SDK.Benchmarking.Results;

namespace DeltaWare.SDK.Benchmarking
{
    internal class BenchmarkBuilder : IBenchmarkBuilder
    {
        private readonly IDictionary<string, Metric> _metrics = new Dictionary<string, Metric>();

        private readonly BenchmarkResult _benchmarkResult;

        public BenchmarkBuilder(string name, string description = null)
        {
            _benchmarkResult = new BenchmarkResult(name, description);
        }

        public IBenchmarkResult Results => _benchmarkResult;

        public IMetricMeasure AddMetric(string name, string description = null)
        {
            if (!_metrics.TryGetValue(name, out Metric metric))
            {
                metric = new Metric(name, description);

                _benchmarkResult.AddResult(metric.Result);

                _metrics.Add(name, metric);
            }
            
            return metric;
        }

        public void CalculateMetrics()
        {
            long executionTime = 0;

            foreach (Metric metric in _metrics.Values)
            {
                executionTime += metric.GetExecutionTime();
            }

            _benchmarkResult.Update(executionTime);
        }
    }
}
