using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Session
{
    /// <summary>
    /// Access rights
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Rights")]
    public class Rights
    {
        /// <summary>
        /// Name
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Access right
        /// </summary>
        [XmlElement("Access")]
        public uint Access { get; set; }
    }
}