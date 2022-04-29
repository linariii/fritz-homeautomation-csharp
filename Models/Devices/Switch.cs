using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models.Devices
{
    [Serializable]
    [XmlRoot(ElementName = "switch")]
    public class Switch
    {
        [XmlElement("state")]
        public State State { get; set; }

        [XmlElement("mode")]
        public Mode Mode { get; set; }

        [XmlElement("lock")]
        public Lock Lock { get; set; }

        [XmlElement("devicelock")]
        public Lock DeviceLock { get; set; }
    }

}