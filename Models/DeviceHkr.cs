using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class DeviceHkr
    {
        [XmlElement("tist")]
        public byte TempCurrent { get; set; }

        [XmlElement("tsoll")]
        public byte TempTarget { get; set; }

        [XmlElement("absenk")]
        public byte TempNight { get; set; }

        [XmlElement("komfort")]
        public byte TempComfort { get; set; }

        [XmlElement("lock")]
        public byte Lock { get; set; }

        [XmlElement("devicelock")]
        public byte DeviceLock { get; set; }

        [XmlElement("errorcode")]
        public byte ErrorCode { get; set; }

        [XmlElement("windowopenactiv")]
        public byte WindowOpenActive { get; set; }

        [XmlElement("windowopenactiveendtime")]
        public byte WindowOpenActiveEndTime { get; set; }

        [XmlElement("boostactive")]
        public byte BoostActive { get; set; }

        [XmlElement("boostactiveendtime")]
        public byte BoostActiveEndTime { get; set; }

        [XmlElement("batterylow")]
        public byte BatteryLow { get; set; }

        [XmlElement("battery")]
        public byte Battery { get; set; }

        [XmlElement("nextchange")]
        public DeviceHkrNextChange NextChange { get; set; }

        [XmlElement("summeractive")]
        public byte SummerActive { get; set; }

        [XmlElement("holidayactive")]
        public byte HolidayActive { get; set; }
    }
}