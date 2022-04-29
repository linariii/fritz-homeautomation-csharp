using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "nextchange")]
    public class DeviceHkrNextChange
    {
        [XmlElement("endperiod")]
        public int EndPeriod { get; set; }

        [XmlElement("tchange")]
        public int TChange { get; set; }
    }
}