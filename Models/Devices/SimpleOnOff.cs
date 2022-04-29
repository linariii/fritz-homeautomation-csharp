using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "simpleonoff")]
    public class SimpleOnOff
    {
        [XmlElement("state")]
        public State State { get; set; }
    }
}
