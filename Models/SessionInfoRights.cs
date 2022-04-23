using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class SessionInfoRights
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Access")]
        public int Access { get; set; }
    }
}