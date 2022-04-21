using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceHumidity
    {
        [XmlElement("rel_humidity")]
        public byte Rel_humidity { get; set; }
    }
}
