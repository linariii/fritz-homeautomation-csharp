using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    [XmlRoot(ElementName = "voltage")]
    public class Voltage
    {
        [XmlElement(ElementName = "stats")]
        public Statistics Statistics { get; set; }
    }
}