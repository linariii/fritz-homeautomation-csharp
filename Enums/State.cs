using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum State
    {
        [XmlEnum(Name = "0")]
        Off,

        [XmlEnum(Name = "1")]
        On,

        [XmlEnum(Name = "inval")]
        Unknown
    }
}