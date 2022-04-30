using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Battery state
    /// </summary>
    public enum Battery
    {
        /// <summary>
        /// Battery charge is fine
        /// </summary>
        [XmlEnum(Name = "0")]
        Ok,

        /// <summary>
        /// Battery charge is low (Battery needs to be replace)
        /// </summary>
        [XmlEnum(Name = "1")]
        Low
    }
}