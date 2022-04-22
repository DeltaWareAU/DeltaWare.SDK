using DeltaWare.SDK.Benchmarking.Metrics.Builder;

namespace DeltaWare.SDK.Benchmarking
{
    public interface IBenchmarkBuilder
    {
        IMetricMeasure AddMetric(string name, string description = null);
    }
}
