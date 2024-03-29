﻿using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Enums
{
    /// <summary>
    /// Active state
    /// </summary>
    public enum Active
    {
        /// <summary>
        /// Device is inactive
        /// </summary>
        [XmlEnum(Name = "0")]
        Inactive,

        /// <summary>
        /// Device is active
        /// </summary>
        [XmlEnum(Name = "1")]
        Active
    }
}