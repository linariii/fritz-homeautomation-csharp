using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Device states
    /// </summary>
    public enum State
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
        /// Device state is unknown
        /// </summary>
        [XmlEnum(Name = "inval")]
        Unknown
    }
}