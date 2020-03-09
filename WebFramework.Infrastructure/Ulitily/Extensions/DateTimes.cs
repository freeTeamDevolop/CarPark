using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Infrastructure.Ulitily.Extensions
{
    public static class DateTimes
    {
        public static long ToUnixTimeStamp(this DateTime utcDateTime, bool formatToTomorrow = false, int timeZone = 8)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (formatToTomorrow)
            {
                utcDateTime = new DateTime(utcDateTime.Year, utcDateTime.Month, utcDateTime.Day, 23, 59, 59,
                    DateTimeKind.Utc);
                timeZone = -timeZone;
                utcDateTime = utcDateTime.AddHours(timeZone);
            }
            var seconds = (utcDateTime - dtDateTime).TotalSeconds;
            return (long)seconds;
        }
    }
}
