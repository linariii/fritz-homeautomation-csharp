using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum Battery
    {
        [XmlEnum(Name = "0")]
        Ok,

        [XmlEnum(Name = "1")]
        Low
    }
}