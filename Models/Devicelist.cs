using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot("devicelist")]
    public class DeviceList
    {
        [XmlElement("device")]
        public List<Device> Devices { get; set; }

        [XmlAttribute("version")]
        public int Version { get; set; }

        [XmlAttribute("fwversion")]
        public decimal FwVersion { get; set; }
    }
}
