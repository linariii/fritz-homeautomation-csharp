using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Models.Session;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class SessionInfo
    {
        [XmlElement("SID")]
        public string SessionId { get; set; }

        [XmlElement("Challenge")]
        public string Challenge { get; set; }

        [XmlElement("BlockTime")]
        public uint BlockTime { get; set; }

        [XmlElement("Rights")]
        public SessionInfoRights Rights { get; set; }

        [XmlElement("Users")]
        public SessionInfoUsers Users { get; set; }
    }
}