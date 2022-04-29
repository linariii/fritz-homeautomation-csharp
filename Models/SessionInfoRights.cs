using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "Rights")]
    public class SessionInfoRights
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Access")]
        public int Access { get; set; }
    }
}