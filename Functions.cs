using System;

namespace Fritz.HomeAutomation
{
    [Flags]
    public enum Functions
    {
        /// <summary>
        /// Bit 0: HAN-FUN Gerät
        /// </summary>
        HanFun = 0,

        /// <summary>
        /// Bit 2: Licht/Lampe
        /// </summary>
        Light = 1 << 2,

        /// <summary>
        /// Bit 4: Alarm-Sensor
        /// </summary>
        Alarm = 1 << 4,

        /// <summary>
        /// Bit 5: AVM-Button
        /// </summary>
        Button = 1 << 5,

        /// <summary>
        /// Bit 6: Heizkörperregler
        /// </summary>
        Thermostat = 1 << 6,

        /// <summary>
        /// Bit 7: Energie Messgerät
        /// </summary>
        EnergyMeter = 1 << 7,

        /// <summary>
        /// Bit 8: Temperatursensor
        /// </summary>
        TemperatureSensor = 1 << 8,

        /// <summary>
        /// Bit 9: Schaltsteckdose
        /// </summary>
        Outlet = 1 << 9,

        /// <summary>
        /// Bit 10: AVM DECT Repeater
        /// </summary>
        DectRepeater = 1 << 10,

        /// <summary>
        /// Bit 11: Mikrofon
        /// </summary>
        Microfone = 1 << 11,

        /// <summary>
        /// Bit 13: HAN-FUN-Unit
        /// </summary>
        HanFunUnit = 1 << 13,

        /// <summary>
        /// Bit 15: an-/ausschaltbares Gerät/Steckdose/Lampe/Aktor
        /// </summary>
        SwitchControl = 1 << 15,

        /// <summary>
        /// Bit 16: Gerät mit einstellbarem Dimm-, Höhen- bzw. Niveau-Level
        /// </summary>
        LevelControl = 1 << 16,

        /// <summary>
        /// Bit 17: Lampe mit einstellbarer Farbe/Farbtemperatur
        /// </summary>
        ColorControl = 1 << 17,

        /// <summary>
        /// Bit 18: Rollladen(Blind) - hoch, runter, stop und level 0% bis 100 %
        /// </summary>
        Shutter = 1 << 18
	}
}