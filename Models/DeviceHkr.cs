using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class DeviceHkr
    {
        [XmlElement("tist")]
        public byte Tist { get; set; }

        [XmlElement("tsoll")]
        public byte Tsoll { get; set; }

        [XmlElement("absenk")]
        public byte Absenk { get; set; }

        [XmlElement("komfort")]
        public byte Komfort { get; set; }

        [XmlElement("lock")]
        public byte Lock { get; set; }

        [XmlElement("devicelock")]
        public byte Devicelock { get; set; }

        [XmlElement("errorcode")]
        public byte Errorcode { get; set; }

        [XmlElement("windowopenactiv")]
        public byte Windowopenactiv { get; set; }

        [XmlElement("windowopenactiveendtime")]
        public byte Windowopenactiveendtime { get; set; }

        [XmlElement("boostactive")]
        public byte Boostactive { get; set; }

        [XmlElement("boostactiveendtime")]
        public byte Boostactiveendtime { get; set; }

        [XmlElement("batterylow")]
        public byte Batterylow { get; set; }

        [XmlElement("battery")]
        public byte Battery { get; set; }

        [XmlElement("nextchange")]
        public DeviceHkrNextchange Nextchange { get; set; }

        [XmlElement("summeractive")]
        public byte Summeractive { get; set; }

        [XmlElement("holidayactive")]
        public byte Holidayactive { get; set; }
    }
}

