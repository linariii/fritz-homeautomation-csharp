using System;

namespace Fritz.HomeAutomation
{
    [Flags]
    public enum Functions
    {
        HanFun = 0,
        Light = 4,
        Alarm = 16,
        Button = 32,
        Thermostat = 64,
        EnergyMeter = 128,
        TemperatureSensor = 256,
        Outlet = 512,
        DectRepeater = 1024,
        Microfone = 2048,
        HanFunUnit = 8192,
        SwitchControl = 32768,
        LevelControl = 65536,
        ColorControl = 131072,
        Shutter = 262144
	}
}