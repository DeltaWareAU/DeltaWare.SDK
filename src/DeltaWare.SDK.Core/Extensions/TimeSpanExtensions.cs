// ReSharper disable once CheckNamespace

using DeltaWare.SDK.Core.Helpers;

namespace System
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="TimeSpan"/> into a human readable string.
        /// </summary>
        /// <param name="span">Specifies the <see cref="TimeSpan"/>.</param>
        /// <param name="significantDigits">Specifies how many decimal places should be displayed.</param>
        public static string ToHumanReadableString(this TimeSpan span, bool asUnits = true, byte significantDigits = 3)
        {
            string format = "G" + significantDigits;

            if (span.Ticks < 10)
            {
                if (asUnits)
                {
                    return TimeUnitsHelper.ConvertMillisecondsToNanoseconds(span.TotalMilliseconds).ToString(format) + " ns";
                }

                return TimeUnitsHelper.ConvertMillisecondsToNanoseconds(span.TotalMilliseconds).ToString(format) + " nanoseconds";
            }

            if (span.Ticks < 10000)
            {
                if (asUnits)
                {
                    return TimeUnitsHelper.ConvertMillisecondsToMicroseconds(span.TotalMilliseconds).ToString(format) + " µs";
                }

                return TimeUnitsHelper.ConvertMillisecondsToMicroseconds(span.TotalMilliseconds).ToString(format) + " microseconds";
                
            }

            if (span.TotalMilliseconds < 1000)
            {
                if (asUnits)
                {
                    return span.TotalMilliseconds.ToString(format) + " ms";
                }

                return span.TotalMilliseconds.ToString(format) + " milliseconds";
            }

            if (span.TotalSeconds < 60)
            {
                if (asUnits)
                {
                    return span.TotalSeconds.ToString(format) + " s";
                }

                return span.TotalSeconds.ToString(format) + " seconds";
            }

            if (span.TotalMinutes < 60)
            {
                if (asUnits)
                {
                    return span.TotalMinutes.ToString(format) + " m";
                }

                return span.TotalMinutes.ToString(format) + " minutes";
            }

            if (span.TotalHours < 24)
            {
                if (asUnits)
                {
                    return span.TotalHours.ToString(format) + " h";
                }

                return span.TotalHours.ToString(format) + " hours";
            }

            if (asUnits)
            {
                return span.TotalDays.ToString(format) + " d";
            }

            return span.TotalDays.ToString(format) + " days";
        }
    }
}