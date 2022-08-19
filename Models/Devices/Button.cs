using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Button
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "button")]
    public class Button
    {
        /// <summary>
        /// Name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// last pressed timestamp
        /// </summary>
        [XmlElement("lastpressedtimestamp")]
        public string LastPressedTimestamp { get; set; }

        /// <summary>
        /// Identifier
        /// </summary>
        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [XmlAttribute("id")]
        public uint Id { get; set; }
    }
}