using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [XmlRoot(ElementName = "temperature")]
    public class Temperature
    {
        [XmlElement(ElementName = "stats")]
        public Stats Stats { get; set; }
    }
}