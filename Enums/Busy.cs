using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Busy states
    /// </summary>
    public enum Busy
    {
        /// <summary>
        /// Device is ready
        /// </summary>
        [XmlEnum(Name = "0")]
        Ready,

        /// <summary>
        /// Device is busy
        /// </summary>
        [XmlEnum(Name = "1")]
        Busy
    }
}