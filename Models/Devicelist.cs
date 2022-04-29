using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "devicelist")]
    public class DeviceList
    {
        [XmlElement("device")]
        public List<Device> Devices { get; set; }

        [XmlAttribute("version")]
        public uint Version { get; set; }

        [XmlAttribute("fwversion")]
        public string FirmwareVersion { get; set; }
    }
}
