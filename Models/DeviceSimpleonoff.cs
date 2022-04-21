using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceSimpleonoff
    {
        [XmlElement("state")]
        public byte State { get; set; }
    }
}
