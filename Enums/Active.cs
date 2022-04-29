using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum Active
    {
        [XmlEnum(Name = "0")]
        Inactive,

        [XmlEnum(Name = "1")]
        Active
    }
}