using Xunit;

namespace Fritz.HomeAutomation.Tests
{
    public class FunctionsTest
    {
        [Fact]
        public void TestBitmask1()
        {
            var functions = (Functions)320;
            Assert.True(functions.HasFlag(Functions.TemperatureSensor));
            Assert.True(functions.HasFlag(Functions.Thermostat));
        }

        [Fact]
        public void TestBitmask2()
        {
            var functions = (Functions)35712;
            Assert.True(functions.HasFlag(Functions.TemperatureSensor));
            Assert.True(functions.HasFlag(Functions.EnergyMeter));
            Assert.True(functions.HasFlag(Functions.Microfone));
            Assert.True(functions.HasFlag(Functions.SwitchControl));
            Assert.True(functions.HasFlag(Functions.Outlet));
        }

        [Fact]
        public void TestBitmask3()
        {
            var functions = (Functions)1048864;
            Assert.True(functions.HasFlag(Functions.TemperatureSensor));
            Assert.True(functions.HasFlag(Functions.Button));
        }
    }
}