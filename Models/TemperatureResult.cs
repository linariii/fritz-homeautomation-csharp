using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models
{
    /// <summary>
    /// Temperature result
    /// </summary>
    public class TemperatureResult
    {
        /// <summary>
        /// current temperature
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// current state
        /// </summary>
        public ThermostatState State { get; set; }
    }
}