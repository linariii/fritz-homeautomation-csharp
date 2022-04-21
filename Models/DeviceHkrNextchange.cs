using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceHkrNextchange
    {
        [XmlElement("endperiod")]
        public int Endperiod
        {
            get; set;
        }

        [XmlElement("tchange")]
        public byte Tchange
        {
            get; set;
        }
    }
}

