using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [XmlRoot(ElementName = "energy")]
    public class Energy
    {
        [XmlElement(ElementName = "stats")]
        public List<Stats> Stats { get; set; }
    }
}