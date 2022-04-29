using System;

namespace Fritz.HomeAutomation.Enums
{
    [Flags]
    public enum Functions
    {
        /// <summary>
        /// Bit 0: HanFun
        /// </summary>
        HanFun = 0,

        /// <summary>
        /// Bit 2: Light
        /// </summary>
        Light = 1 << 2,

        /// <summary>
        /// Bit 4: Alarm
        /// </summary>
        Alarm = 1 << 4,

        /// <summary>
        /// Bit 5: AVM-Button
        /// </summary>
        Button = 1 << 5,

        /// <summary>
        /// Bit 6: Thermostat
        /// </summary>
        Thermostat = 1 << 6,

        /// <summary>
        /// Bit 7: EnergyMeter
        /// </summary>
        EnergyMeter = 1 << 7,

        /// <summary>
        /// Bit 8: Temperature sensor
        /// </summary>
        TemperatureSensor = 1 << 8,

        /// <summary>
        /// Bit 9: Outlet
        /// </summary>
        Outlet = 1 << 9,

        /// <summary>
        /// Bit 10: AVM DECT Repeater
        /// </summary>
        DectRepeater = 1 << 10,

        /// <summary>
        /// Bit 11: Microfone
        /// </summary>
        Microfone = 1 << 11,

        /// <summary>
        /// Bit 13: HanFunUnit
        /// </summary>
        HanFunUnit = 1 << 13,

        /// <summary>
        /// Bit 15: Switch control
        /// </summary>
        SwitchControl = 1 << 15,

        /// <summary>
        /// Bit 16: Level control
        /// </summary>
        LevelControl = 1 << 16,

        /// <summary>
        /// Bit 17: Color control
        /// </summary>
        ColorControl = 1 << 17,

        /// <summary>
        /// Bit 18: Shutter
        /// </summary>
        Shutter = 1 << 18
	}
}