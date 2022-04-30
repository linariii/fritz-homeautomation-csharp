using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Thermostat
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "hkr")]
    public class Thermostat
    {
        /// <summary>
        /// current temperature
        /// </summary>
        [XmlElement("tist")]
        public uint CurrentTemperature { get; set; }

        /// <summary>
        /// target temperature
        /// </summary>
        [XmlElement("tsoll")]
        public uint TargetTemperature { get; set; }

        /// <summary>
        /// economy temperature
        /// </summary>
        [XmlElement("absenk")]
        public uint EconomyTemperature { get; set; }

        /// <summary>
        /// comfort temperature
        /// </summary>
        [XmlElement("komfort")]
        public uint ComfortTemperature { get; set; }

        /// <summary>
        /// Lock state
        /// </summary>
        [XmlElement("lock")]
        public Lock Lock { get; set; }

        /// <summary>
        /// Device lock state
        /// </summary>
        [XmlElement("devicelock")]
        public Lock DeviceLock { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        [XmlElement("errorcode")]
        public ThermostatError Error { get; set; }

        /// <summary>
        /// window open state
        /// </summary>
        [XmlElement("windowopenactiv")]
        public Active WindowOpenActive { get; set; }

        /// <summary>
        /// window open end timestamp
        /// </summary>
        [XmlElement("windowopenactiveendtime")]
        public ulong WindowOpenActiveEndTime { get; set; }

        /// <summary>
        /// boots active state
        /// </summary>
        [XmlElement("boostactive")]
        public Active BoostActive { get; set; }

        /// <summary>
        /// boost active end timestamp
        /// </summary>
        [XmlElement("boostactiveendtime")]
        public ulong BoostActiveEndTime { get; set; }

        /// <summary>
        /// battery state
        /// </summary>
        [XmlElement("batterylow")]
        public Battery BatteryState { get; set; }

        /// <summary>
        /// battery charge
        /// </summary>
        [XmlElement("battery")]
        public uint BatteryCharge { get; set; }

        /// <summary>
        /// next change
        /// </summary>
        [XmlElement("nextchange")]
        public NextChange NextChange { get; set; }

        /// <summary>
        /// Summer active state
        /// </summary>
        [XmlElement("summeractive")]
        public Active SummerActive { get; set; }

        /// <summary>
        /// holiday active state
        /// </summary>
        [XmlElement("holidayactive")]
        public Active HolidayActive { get; set; }
    }
}