// ReSharper disable once CheckNamespace
namespace System
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="TimeSpan"/> into a human readable string.
        /// </summary>
        /// <param name="span">Specifies the <see cref="TimeSpan"/>.</param>
        /// <param name="significantDigits">Specifies how many decimal places should be displayed.</param>
        public static string ToHumanReadableString(this TimeSpan span, byte significantDigits = 3)
        {
            string format = "G" + significantDigits;

            if (span.TotalMilliseconds < 1000)
            {
                return span.TotalMilliseconds.ToString(format) + " Milliseconds";
            }

            if (span.TotalSeconds < 60)
            {
                return span.TotalSeconds.ToString(format) + " Seconds";
            }

            if (span.TotalMinutes < 60)
            {
                return span.TotalMinutes.ToString(format) + " Minutes";
            }

            if (span.TotalHours < 24)
            {
                return span.TotalHours.ToString(format) + " Hours";
            }

            return span.TotalDays.ToString(format) + " Days";
        }
    }
}