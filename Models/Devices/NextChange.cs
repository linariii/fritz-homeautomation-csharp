using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "nextchange")]
    public class NextChange
    {
        [XmlElement("endperiod")]
        public ulong EndPeriod { get; set; }

        [XmlElement("tchange")]
        public uint TargetTemperature { get; set; }
    }
}