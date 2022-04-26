namespace DeltaWare.SDK.Benchmarking.Metrics
{
    public interface IBenchmarkMetrics<out TMetric> where TMetric : IMetric
    {
        TMetric AddMetric(string name, string description = null);
    }
}
