using System;

namespace Fritz.HomeAutomation.Utils
{
    public static class TimeUtils
    {
        public static long TimeToApi(int minutes)
        {
            if (minutes < Constants.MinDurationInMinutes)
                return Constants.MinDurationInMinutes;

            if (minutes > Constants.MaxDurationInMinutes)
                minutes = Constants.MaxDurationInMinutes;

            var dateTime = DateTime.Now.AddMinutes(minutes);

            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

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