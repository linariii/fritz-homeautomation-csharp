using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    [XmlRoot(ElementName = "temperature")]
    public class Temperature
    {
        [XmlElement(ElementName = "stats")]
        public Statistics Statistics { get; set; }
    }
}