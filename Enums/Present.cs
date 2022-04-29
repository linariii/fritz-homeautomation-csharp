using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum Present
    {
        [XmlEnum(Name = "0")]
        NotPresent,

        [XmlEnum(Name = "1")]
        Present
    }
}