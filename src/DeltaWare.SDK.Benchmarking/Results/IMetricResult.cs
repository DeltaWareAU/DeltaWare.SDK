using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Benchmarking.Results
{
    public interface IMetricResult
    {
        public string Name { get; }

        public string Description { get; }

        public long TotalTicks { get; }

        public long Iterations { get; }

        public long MaximumTicks { get; }

        public long MinimumTicks { get; }

        public decimal AverageTicks { get; }
    }
}
