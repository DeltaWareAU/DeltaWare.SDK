using System.Collections.Generic;

namespace DeltaWare.SDK.Benchmarking.Results
{
    public interface IBenchmarkResult : IMetricResult
    {
        public IReadOnlyList<IMetricResult> Results { get; }
    }
}
