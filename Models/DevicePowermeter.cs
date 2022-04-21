using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DevicePowermeter
    {
        [XmlElement("voltage")]
        public int Voltage { get; set; }

        [XmlElement("power")]
        public int Power { get; set; }

        [XmlElement("energy")]
        public int Energy { get; set; }
    }
}
