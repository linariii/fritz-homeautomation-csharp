using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeviceSwitch
    {
        [XmlElement("state")]
        public int State { get; set; }

        [XmlElement("mode")]
        public string Mode { get; set; }

        [XmlElement("lock")]
        public int Lock { get; set; }

        [XmlElement("devicelock")]
        public int DeviceLock { get; set; }
    }

}