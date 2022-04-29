using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Session
{
    [Serializable]
    [XmlRoot(ElementName = "Rights")]
    public class SessionInfoRights
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Access")]
        public uint Access { get; set; }
    }
}