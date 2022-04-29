using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Session
{
    [Serializable]
    [XmlRoot(ElementName = "Users")]
    public class SessionInfoUsers
    {
        [XmlElement("User")]
        public string User { get; set; }
    }
}