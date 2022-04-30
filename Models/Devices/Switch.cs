using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Switch
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "switch")]
    public class Switch
    {
        /// <summary>
        /// State
        /// </summary>
        [XmlElement("state")]
        public State State { get; set; }

        /// <summary>
        /// Mode state
        /// </summary>
        [XmlElement("mode")]
        public SwitchMode SwitchMode { get; set; }

        /// <summary>
        /// Lock state
        /// </summary>
        [XmlElement("lock")]
        public Lock Lock { get; set; }

        /// <summary>
        /// device lock state
        /// </summary>
        [XmlElement("devicelock")]
        public Lock DeviceLock { get; set; }
    }
}