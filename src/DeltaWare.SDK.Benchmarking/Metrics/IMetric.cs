using System;

namespace DeltaWare.SDK.Benchmarking.Metrics
{
    public interface IMetric
    {
        void Measure(Action action);
    }
}
