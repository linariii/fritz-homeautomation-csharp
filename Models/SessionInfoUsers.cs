using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "Users")]
    public class SessionInfoUsers
    {
        [XmlElement("User")]
        public string User { get; set; }
    }
}