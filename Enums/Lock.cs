using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    public enum Lock
    {
        [XmlEnum(Name = "0")]
        Unlocked,

        [XmlEnum(Name = "1")]
        Locked
    }
}