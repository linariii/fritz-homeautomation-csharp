using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class SessionInfoUsers
    {
        [XmlElement("User")]
        public string User { get; set; }
    }
}