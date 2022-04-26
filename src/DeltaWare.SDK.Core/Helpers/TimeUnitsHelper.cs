namespace DeltaWare.SDK.Core.Helpers
{
    public class TimeUnitsHelper
    {
        public const double NanosecondsInMillisecond = 1000000;
        public const double NanosecondsInMicrosecond = 1000;
        public const double MicrosecondsInMillisecond = 1000;
        public const double MicrosecondsInNanosecond = 0.001;
        public const double MillisecondsInNanosecond = 0.000001;
        public const double MillisecondsInMicrosecond = 0.001;

        public static double ConvertMillisecondsToNanoseconds(double milliseconds)
        {
            return milliseconds * NanosecondsInMillisecond;
        }

        public static double ConvertMicrosecondsToNanoseconds(double microseconds)
        {
            return microseconds * NanosecondsInMicrosecond;
        }

        public static double ConvertMillisecondsToMicroseconds(double milliseconds)
        {
            return milliseconds * MicrosecondsInMillisecond;
        }

        public static double ConvertNanosecondsToMilliseconds(double nanoseconds)
        {
            return nanoseconds * MillisecondsInNanosecond;
        }

        public static double ConvertMicrosecondsToMilliseconds(double microseconds)
        {
            return microseconds * MillisecondsInMicrosecond;
        }

        public static double ConvertNanosecondsToMicroseconds(double nanoseconds)
        {
            return nanoseconds * MicrosecondsInNanosecond;
        }
    }
}
