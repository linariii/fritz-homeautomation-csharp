using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [XmlRoot(ElementName = "power")]
    public class Power
    {
        [XmlElement(ElementName = "stats")]
        public Stats Stats { get; set; }
    }
}