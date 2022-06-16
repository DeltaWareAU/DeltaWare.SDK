namespace DeltaWare.SDK.Benchmarking
{
    internal class UxTesting
    {
        public void Test1()
        {
            Benchmark benchmark = new Benchmark(builder =>
            {
                int a = 5;
                int b = 10;
                int c = 0;

                builder.AddMetric("Addition")
                    .Measure(() =>
                    {
                        c = a * b;
                    });
            });

            benchmark.Run();
        }
    }
}
