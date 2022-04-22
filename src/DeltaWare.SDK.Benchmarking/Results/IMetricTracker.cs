namespace DeltaWare.SDK.Benchmarking.Results
{
    public interface IMetricTracker
    {
        void Update(long ticks);
    }
}
