using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// On/Off
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "simpleonoff")]
    public class SimpleOnOff
    {
        /// <summary>
        /// State
        /// </summary>
        [XmlElement("state")]
        public State State { get; set; }
    }
}
