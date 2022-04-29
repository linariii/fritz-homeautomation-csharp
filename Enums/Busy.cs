using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum Busy
    {
        [XmlEnum(Name = "0")]
        Ready,

        [XmlEnum(Name = "1")]
        Busy
    }
}