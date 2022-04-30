using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models.Session
{
    /// <summary>
    /// Users
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Users")]
    public class Users
    {
        /// <summary>
        /// User
        /// </summary>
        [XmlElement("User")]
        public string User { get; set; }
    }
}