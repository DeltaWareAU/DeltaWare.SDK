using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Benchmarking.Metrics
{
    public interface IAsyncMetric: IMetric
    {
        Task Measure(Func<Task> action);
    }
}
