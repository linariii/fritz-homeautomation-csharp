using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Temperature
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "temperature")]
    public class Temperature
    {
        /// <summary>
        /// Temperature in °C
        /// </summary>
        [XmlElement("celsius")]
        public uint Celsius { get; set; }

        /// <summary>
        /// offset
        /// </summary>
        [XmlElement("offset")]
        public uint Offset { get; set; }
    }
}