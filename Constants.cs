namespace Fritz.HomeAutomation
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// value for thermostat on
        /// </summary>
        public const int TemperatureOn = 254;

        /// <summary>
        /// value for thermostat off
        /// </summary>
        public const int TemperatureOff = 253;

        /// <summary>
        /// min temperature supported by the api
        /// </summary>
        public const int MinTemperature = 8;

        /// <summary>
        /// max temperature supported by the api
        /// </summary>
        public const int MaxTemperature = 28;


        /// <summary>
        /// max duration for boost or window open 
        /// </summary>
        public const int MaxDurationInMinutes = 1440; //24h

        /// <summary>
        /// min duration for boost or windwo open
        /// </summary>
        public const int MinDurationInMinutes = 1; //1 minute
    }
}