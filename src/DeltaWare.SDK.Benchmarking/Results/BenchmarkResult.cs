namespace DeltaWare.SDK.Benchmarking.Results
{
    internal class BenchmarkResult : MetricResult
    {
        public BenchmarkResult(string name, string description = null) : base(name, description)
        {
        }

        public override void Update(long ticks)
        {
            base.Update(ticks);
            base.Update();
        }
    }
}
