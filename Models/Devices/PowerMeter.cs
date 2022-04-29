using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "powermeter")]
    public class PowerMeter
    {
        [XmlElement("voltage")]
        public uint Voltage { get; set; }

        [XmlElement("power")]
        public uint Power { get; set; }

        [XmlElement("energy")]
        public uint Energy { get; set; }
    }
}
