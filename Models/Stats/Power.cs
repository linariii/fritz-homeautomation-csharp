using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    [XmlRoot(ElementName = "power")]
    public class Power
    {
        [XmlElement(ElementName = "stats")]
        public Statistics Statistics { get; set; }
    }
}