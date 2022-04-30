using System.Linq;
using System.Threading.Tasks;
using Fritz.HomeAutomation.Enums;
using Xunit;

namespace Fritz.HomeAutomation.Tests
{
    public class ClientTest
    {
        [Fact]
        public async Task Test()
        {
            var client = new HomeAutomationClient();
            var sid = await client.GetSessionId("daniel", "1Neo1Pain9?");
            var devices = await client.GetFilteredDevices(sid, Functions.Outlet);
            var ain = devices.FirstOrDefault()?.Ain;
            var state = await client.GetSwitchState(sid, ain);
            Assert.NotNull(state);
            Assert.IsType<State>(state);
        }
    }
}