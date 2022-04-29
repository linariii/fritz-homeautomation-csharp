using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum ThermostatError
    {
        [XmlEnum(Name = "0")]
        None,

        [XmlEnum(Name = "1")]
        BadAdaptation,

        [XmlEnum(Name = "2")]
        WeakBatteries,

        [XmlEnum(Name = "3")]
        ValveStuck,

        [XmlEnum(Name = "4")]
        Preparation,

        [XmlEnum(Name = "5")]
        Installation,

        [XmlEnum(Name = "6")]
        Adaptation
    }
}