using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum Mode
    {
        [XmlEnum(Name = "0")]
        Off,

        [XmlEnum(Name = "1")]
        On,

        [XmlEnum(Name = "auto")]
        Auto
    }
}