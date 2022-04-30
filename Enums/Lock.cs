using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Lock states
    /// </summary>
    public enum Lock
    {
        /// <summary>
        /// Device is unlocked
        /// </summary>
        [XmlEnum(Name = "0")]
        Unlocked,

        /// <summary>
        /// Device is locked
        /// </summary>
        [XmlEnum(Name = "1")]
        Locked
    }
}