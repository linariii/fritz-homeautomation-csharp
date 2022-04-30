using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models
{
    public class TemperatureResult
    {
        public double? Temperature { get; set; }
        public ThermostatState State { get; set; }
    }
}