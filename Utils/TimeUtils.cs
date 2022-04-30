using System;

namespace Fritz.HomeAutomation.Utils
{
    /// <summary>
    /// Utils for time conversions
    /// </summary>
    public static class TimeUtils
    {
        /// <summary>
        /// Convert duration to linux timestamp
        /// </summary>
        /// <param name="duration">duration in duration</param>
        /// <returns>Linux timestamp</returns>
        public static long TimeToApi(int duration)
        {
            if (duration < Constants.MinDurationInMinutes)
                return Constants.MinDurationInMinutes;

            if (duration > Constants.MaxDurationInMinutes)
                duration = Constants.MaxDurationInMinutes;

            var dateTime = DateTime.Now.AddMinutes(duration);

            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Convert linux timestamp to datetime
        /// </summary>
        /// <param name="time">linux timestamp</param>
        /// <returns>DateTime</returns>
        public static DateTime? ApiToDatetime(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
                return null;

            if (!long.TryParse(time, out var val))
                return null;

            return DateTimeOffset.FromUnixTimeSeconds(val).LocalDateTime;
        }
    }
}