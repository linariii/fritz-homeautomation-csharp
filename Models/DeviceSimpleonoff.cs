using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "simpleonoff")]
    public class DeviceSimpleOnOff
    {
        [XmlElement("state")]
        public int State { get; set; }
    }
}
