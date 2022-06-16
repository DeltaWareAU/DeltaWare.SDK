using DeltaWare.SDK.Benchmarking.Results;
using System.Collections.Generic;

namespace DeltaWare.SDK.Benchmarking.Metrics
{
    internal class BenchmarkMetrics<TMetricDefinition, TMetric> : IBenchmarkMetrics<TMetric>
        where TMetricDefinition : Metric, TMetric, new()
        where TMetric : IMetric

    {
        private readonly IDictionary<string, TMetricDefinition> _metrics = new Dictionary<string, TMetricDefinition>();

        private readonly BenchmarkResult _result;

        public IBenchmarkResult Result => _result;

        public BenchmarkMetrics(string name, string description = null)
        {
            _result = new BenchmarkResult(name, description);
        }

        public TMetric AddMetric(string name, string description = null)
        {
            if (!_metrics.TryGetValue(name, out TMetricDefinition metric))
            {
                metric = new TMetricDefinition
                {
                    Description = description,
                    Name = name,
                    Result = _result.CreateChild(name, description)

                };

                _metrics.Add(name, metric);
            }

            return metric;
        }

        public void Measure()
        {
            _result.Update(0);
        }
    }
}
