using System;
using System.Xml.Serialization;

namespace Fritz.HomeAutomation.Models
{
    [Serializable]
    [XmlRoot(ElementName = "hkr")]
    public class DeviceHkr
    {
        [XmlElement("tist")]
        public int TempCurrent { get; set; }

        [XmlElement("tsoll")]
        public int TempTarget { get; set; }

        [XmlElement("absenk")]
        public int TempNight { get; set; }

        [XmlElement("komfort")]
        public int TempComfort { get; set; }

        [XmlElement("lock")]
        public int Lock { get; set; }

        [XmlElement("devicelock")]
        public int DeviceLock { get; set; }

        [XmlElement("errorcode")]
        public int ErrorCode { get; set; }

        [XmlElement("windowopenactiv")]
        public int WindowOpenActive { get; set; }

        [XmlElement("windowopenactiveendtime")]
        public int WindowOpenActiveEndTime { get; set; }

        [XmlElement("boostactive")]
        public int BoostActive { get; set; }

        [XmlElement("boostactiveendtime")]
        public int BoostActiveEndTime { get; set; }

        [XmlElement("batterylow")]
        public int BatteryLow { get; set; }

        [XmlElement("battery")]
        public int Battery { get; set; }

        [XmlElement("nextchange")]
        public DeviceHkrNextChange NextChange { get; set; }

        [XmlElement("summeractive")]
        public int SummerActive { get; set; }

        [XmlElement("holidayactive")]
        public int HolidayActive { get; set; }
    }
}