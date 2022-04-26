using System;

namespace DeltaWare.SDK.Core.Helpers
{
    public static class TickHelper
    {
        public static string ToHumanReadableTime(long ticks, bool asUnits = true, byte significantDigits = 3)
        {
            return new TimeSpan(ticks).ToHumanReadableString(asUnits, significantDigits);
        }       
    }
}
