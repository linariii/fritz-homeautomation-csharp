using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;
using Fritz.HomeAutomation.Models.Devices;
using SimpleOnOff = Fritz.HomeAutomation.Models.Devices.SimpleOnOff;

namespace Fritz.HomeAutomation.Models
{
    /// <summary>
    /// Device
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "device")]
    public class Device
    {
        /// <summary>
        /// Id
        /// </summary>
        [XmlAttribute("id")]
        public uint Id { get; set; }

        /// <summary>
        /// Function bitmask
        /// </summary>
        [XmlAttribute("functionbitmask")]
        public uint FunctionBitMask { get; set; }

        /// <summary>
        /// Firmware version
        /// </summary>
        [XmlAttribute("fwversion")]
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// Manufacturer
        /// </summary>
        [XmlAttribute("manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [XmlAttribute("productname")]
        public string ProductName { get; set; }

        /// <summary>
        /// Present state
        /// </summary>
        [XmlElement("present")]
        public Present Present { get; set; }

        /// <summary>
        /// Busy state
        /// </summary>
        [XmlElement("txbusy")]
        public Busy Txbusy { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Battery charge
        /// </summary>
        [XmlElement("battery")]
        public uint? BatteryCharge { get; set; }

        /// <summary>
        /// Battery state
        /// </summary>
        [XmlElement("batterylow")]
        public Battery? BatteryState { get; set; }

        /// <summary>
        /// Switch information
        /// </summary>
        [XmlElement("switch")]
        public Switch Switch { get; set; }

        /// <summary>
        /// On/Off information
        /// </summary>
        [XmlElement("simpleonoff")]
        public SimpleOnOff SimpleOnOff { get; set; }

        /// <summary>
        /// Power meter information
        /// </summary>
        [XmlElement("powermeter")]
        public PowerMeter PowerMeter { get; set; }

        /// <summary>
        /// Temperature information
        /// </summary>
        [XmlElement("temperature")]
        public Temperature Temperature { get; set; }

        /// <summary>
        /// Humidity information
        /// </summary>
        [XmlElement("humidity")]
        public Humidity Humidity { get; set; }

        /// <summary>
        /// List of buttons
        /// </summary>
        [XmlElement("button")]
        public List<Button> Buttons { get; set; }

        /// <summary>
        /// Thermostat information
        /// </summary>
        [XmlElement("hkr")]
        public Thermostat Thermostat { get; set; }

        /// <summary>
        /// Device identifier
        /// </summary>
        [XmlAttribute("identifier")]
        public string Ain { get; set; }

        /// <summary>
        /// Supported device functions
        /// </summary>
        [XmlIgnore]
        public Functions? Functions => (Functions)FunctionBitMask;
    }
}