using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    [XmlRoot(ElementName = "energy")]
    public class Energy
    {
        [XmlElement(ElementName = "stats")]
        public List<Statistics> Statistics { get; set; }
    }
}