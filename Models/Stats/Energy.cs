using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Stats
{
    /// <summary>
    /// Energie stats
    /// </summary>
    [XmlRoot(ElementName = "energy")]
    public class Energy
    {
        /// <summary>
        /// Stats
        /// </summary>
        [XmlElement(ElementName = "stats")]
        public List<Statistics> Statistics { get; set; }
    }
}