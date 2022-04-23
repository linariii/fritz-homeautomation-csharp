using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [XmlRoot(ElementName = "stats")]
    public class Stats
    {
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }

        [XmlAttribute(AttributeName = "grid")]
        public string Grid { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}