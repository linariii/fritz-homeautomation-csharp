using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class SessionInfo
    {
        [XmlElement("SID")]
        public string Sid { get; set; }

        [XmlElement("Challenge")]
        public string Challenge { get; set; }

        [XmlElement("BlockTime")]
        public byte BlockTime { get; set; }

        [XmlElement("Rights")]
        public SessionInfoRights Rights { get; set; }

        [XmlElement("Users")]
        public SessionInfoUsers Users { get; set; }
    }
}