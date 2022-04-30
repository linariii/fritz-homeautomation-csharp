using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Humidity
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "humidity")]
    public class Humidity
    {
        /// <summary>
        /// relative humidity
        /// </summary>
        [XmlElement("rel_humidity")]
        public uint RelativeHumidity { get; set; }
    }
}
