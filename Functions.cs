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
        Light = 4,

        /// <summary>
        /// Bit 4: Alarm-Sensor
        /// </summary>
        Alarm = 16,

        /// <summary>
        /// Bit 5: AVM-Button
        /// </summary>
        Button = 32,

        /// <summary>
        /// Bit 6: Heizkörperregler
        /// </summary>
        Thermostat = 64,

        /// <summary>
        /// Bit 7: Energie Messgerät
        /// </summary>
        EnergyMeter = 128,

        /// <summary>
        /// Bit 8: Temperatursensor
        /// </summary>
        TemperatureSensor = 256,

        /// <summary>
        /// Bit 9: Schaltsteckdose
        /// </summary>
        Outlet = 512,

        /// <summary>
        /// Bit 10: AVM DECT Repeater
        /// </summary>
        DectRepeater = 1024,

        /// <summary>
        /// Bit 11: Mikrofon
        /// </summary>
        Microfone = 2048,

        /// <summary>
        /// Bit 13: HAN-FUN-Unit
        /// </summary>
        HanFunUnit = 8192,

        /// <summary>
        /// Bit 15: an-/ausschaltbares Gerät/Steckdose/Lampe/Aktor
        /// </summary>
        SwitchControl = 32768,

        /// <summary>
        /// Bit 16: Gerät mit einstellbarem Dimm-, Höhen- bzw. Niveau-Level
        /// </summary>
        LevelControl = 65536,

        /// <summary>
        /// Bit 17: Lampe mit einstellbarer Farbe/Farbtemperatur
        /// </summary>
        ColorControl = 131072,

        /// <summary>
        /// Bit 18: Rollladen(Blind) - hoch, runter, stop und level 0% bis 100 %
        /// </summary>
        Shutter = 262144
	}
}