using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeviceSimpleOnOff
    {
        [XmlElement("state")]
        public byte State { get; set; }
    }
}
