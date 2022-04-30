using System;
using System.Xml.Serialization;
using Fritz.HomeAutomation.Models.Session;

namespace Fritz.HomeAutomation.Models
{
    /// <summary>
    /// Session info
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class SessionInfo
    {
        /// <summary>
        /// Session Id
        /// </summary>
        [XmlElement("SID")]
        public string SessionId { get; set; }

        /// <summary>
        /// Challenge
        /// </summary>
        [XmlElement("Challenge")]
        public string Challenge { get; set; }

        /// <summary>
        /// Block time
        /// </summary>
        [XmlElement("BlockTime")]
        public uint BlockTime { get; set; }

        /// <summary>
        /// Access rights
        /// </summary>
        [XmlElement("Rights")]
        public Rights Rights { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        [XmlElement("Users")]
        public Users Users { get; set; }
    }
}