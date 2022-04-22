using System;

namespace DeltaWare.SDK.Benchmarking.Metrics.Builder
{
    public interface IMetricMeasure
    {
        void Measure(Action action);
    }
}
