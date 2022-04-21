using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceSwitch
    {
        [XmlElement("state")]
        public byte State { get; set; }

        [XmlElement("mode")]
        public string Mode { get; set; }

        [XmlElement("lock")]
        public byte Lock { get; set; }

        [XmlElement("devicelock")]
        public byte Devicelock { get; set; }
    }

}