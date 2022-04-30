using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Mode of the 
    /// </summary>
    public enum SwitchMode
    {
        /// <summary>
        /// Device is off
        /// </summary>
        [XmlEnum(Name = "0")]
        Off,

        /// <summary>
        /// Device is on
        /// </summary>
        [XmlEnum(Name = "1")]
        On,

        /// <summary>
        /// Device is in auto mode
        /// </summary>
        [XmlEnum(Name = "auto")]
        Auto
    }
}