using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Present states
    /// </summary>
    public enum Present
    {
        /// <summary>
        /// Device is not connected
        /// </summary>
        [XmlEnum(Name = "0")]
        NotPresent,

        /// <summary>
        /// Device is connected
        /// </summary>
        [XmlEnum(Name = "1")]
        Present
    }
}