using DeltaWare.SDK.Benchmarking.Results;
using System;
using System.Threading.Tasks;
using DeltaWare.SDK.Benchmarking.Metrics;

namespace DeltaWare.SDK.Benchmarking
{
    public class AsyncBenchmark
    {
        public string Name { get; set; } = "Benchmark";

        public string Description { get; set; } = string.Empty;

        private readonly Func<IBenchmarkMetrics<IAsyncMetric>, Task> _benchmark;

        public AsyncBenchmark(Func<IBenchmarkMetrics<IAsyncMetric>, Task> benchmark)
        {
            _benchmark = benchmark;
        }

        public async Task<IBenchmarkResult> MeasureAsync(int iterations = 1)
        {
            BenchmarkMetrics<AsyncMetric, IAsyncMetric> metrics = new BenchmarkMetrics<AsyncMetric, IAsyncMetric>(Name, Description);

            for (int i = 0; i < iterations; i++)
            {
                await _benchmark.Invoke(metrics);
                
                metrics.Measure();
            }

            return metrics.Result;
        }
    }
}
