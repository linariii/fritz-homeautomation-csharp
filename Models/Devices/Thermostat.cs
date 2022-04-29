using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "hkr")]
    public class Thermostat
    {
        [XmlElement("tist")]
        public uint CurrentTemperature { get; set; }

        [XmlElement("tsoll")]
        public uint TargetTemperature { get; set; }

        [XmlElement("absenk")]
        public uint EconomyTemperature { get; set; }

        [XmlElement("komfort")]
        public uint ComfortTemperature { get; set; }

        [XmlElement("lock")]
        public Lock Lock { get; set; }

        [XmlElement("devicelock")]
        public Lock DeviceLock { get; set; }

        [XmlElement("errorcode")]
        public ThermostatError Error { get; set; }

        [XmlElement("windowopenactiv")]
        public Active WindowOpenActive { get; set; }

        [XmlElement("windowopenactiveendtime")]
        public ulong WindowOpenActiveEndTime { get; set; }

        [XmlElement("boostactive")]
        public Active BoostActive { get; set; }

        [XmlElement("boostactiveendtime")]
        public ulong BoostActiveEndTime { get; set; }

        [XmlElement("batterylow")]
        public Battery BatteryState { get; set; }

        [XmlElement("battery")]
        public uint BatteryCharge { get; set; }

        [XmlElement("nextchange")]
        public NextChange NextChange { get; set; }

        [XmlElement("summeractive")]
        public Active SummerActive { get; set; }

        [XmlElement("holidayactive")]
        public Active HolidayActive { get; set; }
    }
}