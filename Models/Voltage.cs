using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [XmlRoot(ElementName = "voltage")]
    public class Voltage
    {
        [XmlElement(ElementName = "stats")]
        public Stats Stats { get; set; }
    }
}