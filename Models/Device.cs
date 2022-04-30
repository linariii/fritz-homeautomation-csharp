using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;
using Fritz.HomeAutomation.Models.Devices;
using SimpleOnOff = Fritz.HomeAutomation.Models.Devices.SimpleOnOff;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "device")]
    public class Device
    {
        [XmlAttribute("id")]
        public uint Id { get; set; }

        [XmlAttribute("functionbitmask")]
        public uint FunctionBitMask { get; set; }

        [XmlAttribute("fwversion")]
        public string FirmwareVersion { get; set; }

        [XmlAttribute("manufacturer")]
        public string Manufacturer { get; set; }

        [XmlAttribute("productname")]
        public string ProductName { get; set; }

        [XmlElement("present")]
        public Present Present { get; set; }

        [XmlElement("txbusy")]
        public Busy Txbusy { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("battery")]
        public uint? BatteryCharge { get; set; }

        [XmlElement("batterylow")]
        public Battery? BatteryState { get; set; }

        [XmlElement("switch")]
        public Switch Switch { get; set; }

        [XmlElement("simpleonoff")]
        public SimpleOnOff SimpleOnOff { get; set; }

        [XmlElement("powermeter")]
        public PowerMeter PowerMeter { get; set; }

        [XmlElement("temperature")]
        public Temperature Temperature { get; set; }

        [XmlElement("humidity")]
        public Humidity Humidity { get; set; }

        [XmlElement("button")]
        public List<Button> Buttons { get; set; }

        [XmlElement("hkr")]
        public Thermostat Thermostat { get; set; }

        [XmlAttribute("identifier")]
        public string Ain { get; set; }

        [XmlIgnore]
        public Functions? Functions => (Functions)FunctionBitMask;
    }
}