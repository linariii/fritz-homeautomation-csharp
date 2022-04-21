using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class SessionInfo
    {
        public string SID { get; set; }

        public string Challenge { get; set; }

        public byte BlockTime { get; set; }

        public SessionInfoRights Rights { get; set; }

        public SessionInfoUsers Users { get; set; }
    }
}