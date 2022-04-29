using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class Device
    {
        [XmlElement("present")]
        public int Present { get; set; }

        [XmlElement("txbusy")]
        public int Txbusy { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("battery")]
        public int Battery { get; set; }

        [XmlElement("batterylow")]
        public int BatteryLow { get; set; }

        [XmlElement("switch")]
        public DeviceSwitch Switch { get; set; }

        [XmlElement("simpleonoff")]
        public DeviceSimpleOnOff SimpleOnOff { get; set; }

        [XmlElement("powermeter")]
        public DevicePowerMeter PowerMeter { get; set; }

        [XmlElement("temperature")]
        public DeviceTemperature Temperature { get; set; }

        [XmlElement("humidity")]
        public DeviceHumidity Humidity { get; set; }

        [XmlElement("button")]
        public List<DeviceButton> Buttons { get; set; }

        [XmlElement("device")]
        public DeviceHkr Hkr { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("functionbitmask")]
        public string FunctionBitMask { get; set; }

        [XmlAttribute("fwversion")]
        public decimal FwVersion { get; set; }

        [XmlAttribute("manufacturer")]
        public string Manufacturer { get; set; }

        [XmlAttribute("productname")]
        public string ProductName { get; set; }

        [XmlIgnore]
        public Functions? Functions
        {
            get
            {
                if (int.TryParse(FunctionBitMask, out var value))
                    return (Functions)value;

                return null;
            }
        }
    }
}