using System.Collections.Generic;

namespace DeltaWare.SDK.Benchmarking.Results
{
    public interface IBenchmarkResult
    {
        string Name { get; }

        string Description { get; }

        long TotalTicks { get; }

        long Iterations { get; }

        long MaximumTicks { get; }

        long MinimumTicks { get; }

        decimal AverageTicks { get; }

        public IReadOnlyList<IBenchmarkResult> Results { get; }
    }
}
