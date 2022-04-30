using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Devices
{
    /// <summary>
    /// Next change
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "nextchange")]
    public class NextChange
    {
        /// <summary>
        /// End period timestamp
        /// </summary>
        [XmlElement("endperiod")]
        public ulong EndPeriod { get; set; }

        /// <summary>
        /// Target temperature
        /// </summary>
        [XmlElement("tchange")]
        public uint TargetTemperature { get; set; }
    }
}