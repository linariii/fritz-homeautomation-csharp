using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class SessionInfoRights
    {
        public string Name { get; set; }

        public int Access { get; set; }
    }
}