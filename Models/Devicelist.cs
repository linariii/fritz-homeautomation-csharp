using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    /// <summary>
    /// Device list result
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "devicelist")]
    public class DeviceList
    {
        /// <summary>
        /// List of devices
        /// </summary>
        [XmlElement("device")]
        public List<Device> Devices { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        [XmlAttribute("version")]
        public uint Version { get; set; }

        /// <summary>
        /// Firmware version
        /// </summary>
        [XmlAttribute("fwversion")]
        public string FirmwareVersion { get; set; }
    }
}
