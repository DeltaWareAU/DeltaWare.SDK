using System.Collections.Generic;
using System.Linq;

namespace DeltaWare.SDK.Benchmarking.Results
{
    internal class BenchmarkResult : MetricResult, IBenchmarkResult
    {
        private readonly List<IMetricResult> _results = new();

        public IReadOnlyList<IMetricResult> Results => _results;
        
        public BenchmarkResult(string name, string description = null) : base(name, description)
        {
        }

        public void AddResult(IMetricResult result)
        {
            _results.Add(result);
        }
    }
}
