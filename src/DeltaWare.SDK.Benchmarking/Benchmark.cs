using DeltaWare.SDK.Benchmarking.Results;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Benchmarking
{
    public class Benchmark : IBenchmark
    {
        public string Name { get; set; } = "Benchmark";

        public string Description { get; set; } = string.Empty;

        private readonly Action<IBenchmarkBuilder> _benchmark;

        public Benchmark(Action<IBenchmarkBuilder> benchmark)
        {
            _benchmark = benchmark;
        }

        public IBenchmarkResult Run(int iterations = 1)
        {
            BenchmarkBuilder builder = new BenchmarkBuilder(Name, Description);

            for (int i = 0; i < iterations; i++)
            {
                _benchmark.Invoke(builder);

                builder.CalculateMetrics();
            }

            return builder.Results;
        }

        public Task<IBenchmarkResult> RunAsync(int iterations = 1)
        {
            return Task.FromResult(Run(iterations));
        }
    }

    public interface IBenchmark
    {
        IBenchmarkResult Run(int iterations = 1);

        Task<IBenchmarkResult> RunAsync(int iterations = 1);
    }
}
