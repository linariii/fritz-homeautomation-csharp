using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Power meter
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "powermeter")]
    public class PowerMeter
    {
        /// <summary>
        /// Voltage
        /// </summary>
        [XmlElement("voltage")]
        public uint Voltage { get; set; }

        /// <summary>
        /// Power
        /// </summary>
        [XmlElement("power")]
        public uint Power { get; set; }

        /// <summary>
        /// Energie
        /// </summary>
        [XmlElement("energy")]
        public uint Energy { get; set; }
    }
}
