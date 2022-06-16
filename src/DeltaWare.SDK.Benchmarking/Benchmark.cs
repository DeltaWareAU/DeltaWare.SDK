using DeltaWare.SDK.Benchmarking.Metrics;
using DeltaWare.SDK.Benchmarking.Results;
using System;

namespace DeltaWare.SDK.Benchmarking
{
    public class Benchmark
    {
        public string Name { get; set; } = "Benchmark";

        public string Description { get; set; } = string.Empty;

        private readonly Action<IBenchmarkMetrics<IMetric>> _benchmark;

        public Benchmark(Action<IBenchmarkMetrics<IMetric>> benchmark)
        {
            _benchmark = benchmark;
        }

        public IBenchmarkResult Measure(int iterations = 1)
        {
            BenchmarkMetrics<Metric, IMetric> metrics = new BenchmarkMetrics<Metric, IMetric>(Name, Description);

            for (int i = 0; i < iterations; i++)
            {
                _benchmark.Invoke(metrics);

                metrics.Measure();
            }

            return metrics.Result;
        }
    }
}
