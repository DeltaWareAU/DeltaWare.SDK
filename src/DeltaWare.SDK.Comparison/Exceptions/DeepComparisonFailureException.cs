using DeltaWare.SDK.Comparison.Types;
using System;
using System.Text;

namespace DeltaWare.SDK.Comparison.Exceptions
{
    public class DeepComparisonFailureException : Exception
    {
        public ValueComparisonResult[] FailingResults { get; }

        public DeepComparisonFailureException(ValueComparisonResult[] failingResults) : base(GenerateReportString(failingResults))
        {
            FailingResults = failingResults;
        }

        private static string GenerateReportString(ValueComparisonResult[] failingResults)
        {
            StringBuilder report = new StringBuilder();

            report.Append("{\r\n");
            report.Append($"  \"Total:\" \"{failingResults.Length}\",\r\n");
            report.Append("  \"Failures\": {\r\n");

            foreach (ValueComparisonResult failingComparison in failingResults)
            {
                report.Append($"    \"{failingComparison.Name}\": {{\r\n");
                report.Append($"      \"A\": \"{failingComparison.ValueA}\",\r\n");
                report.Append($"      \"B\": \"{failingComparison.ValueB}\"\r\n");
                report.Append("    },\r\n");
            }

            report.Append("  }");
            report.Append("}");

            return report.ToString();
        }
    }
}