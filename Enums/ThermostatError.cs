using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Thermostat error codes
    /// </summary>
    public enum ThermostatError
    {
        /// <summary>
        /// no error
        /// </summary>
        [XmlEnum(Name = "0")]
        None,

        /// <summary>
        /// No adaptation possible. Device correctly mounted on the radiator?
        /// </summary>
        [XmlEnum(Name = "1")]
        BadAdaptation,

        /// <summary>
        /// Valve stroke too short or battery power too low. Open and close valve tappet by hand several times or several times or insert new batteries.
        /// </summary>
        [XmlEnum(Name = "2")]
        WeakBatteries,

        /// <summary>
        /// No valve movement possible. Valve tappet free?
        /// </summary>
        [XmlEnum(Name = "3")]
        ValveStuck,

        /// <summary>
        /// The installation is being prepared.
        /// </summary>
        [XmlEnum(Name = "4")]
        Preparation,

        /// <summary>
        /// The radiator controller is in installation mode and can be mounted on the heating valve.
        /// </summary>
        [XmlEnum(Name = "5")]
        Installation,

        /// <summary>
        /// The radiator controller now adapts to the stroke of the heating valve
        /// </summary>
        [XmlEnum(Name = "6")]
        Adaptation
    }
}