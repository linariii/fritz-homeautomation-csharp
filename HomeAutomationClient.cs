using Fritz.HomeAutomation.Extensions;
using Fritz.HomeAutomation.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fritz.HomeAutomation
{
    public class HomeAutomationClient
    {
        public string BaseUrl { get; set; }

        public HomeAutomationClient(string baseUrl = "http://fritz.box/")
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// Get session id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> GetSid(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var result = await DownloadString($"{BaseUrl}login_sid.lua?username={username}");
            if (string.IsNullOrEmpty(result))
                return null;

            var sessionInfo = result.Deserialize<SessionInfo>();
            if (sessionInfo.SID == "0000000000000000")
            {
                var response = sessionInfo.Challenge + "-" + GetMD5Hash(sessionInfo.Challenge + "-" + password);
                result = await DownloadString($"{BaseUrl}login_sid.lua?username={username}&response={response}");
                sessionInfo = result.Deserialize<SessionInfo>();
                return sessionInfo?.SID;
            }

            return sessionInfo.SID;
        }

        /// <summary>
        /// List all available devices 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <returns></returns>
        public async Task<Devicelist> GetDevices(string sid)
        {
            if (string.IsNullOrEmpty(sid))
                return null;

            var result = await DownloadString($"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=getdevicelistinfos");
            return string.IsNullOrEmpty(result) 
                ? null 
                : result.Deserialize<Devicelist>();
        }


        /// <summary>
        /// Get current switch state
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns>0 = deactivated, 1 = activated</returns>
        public async Task<int?> GetSwitchState(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var result = await DownloadString($"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchstate&ain={ain}");
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Activate selected switch
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<int?> SetSwitchOn(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var result = await DownloadString($"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchon&ain={ain}");
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Deactivate selected switch
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<int?> SetSwitchOff(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var result = await DownloadString("{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchoff&ain={ain}");
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Get current power usage from selected device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<int?> GetSwitchPower(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var result = await DownloadString($"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchpower&ain={ain}");
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var power))
                return null;

            return power;
        }

        /// <summary>
        /// Get current temperature of the selected device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<double?> GetTemperature(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var result = await DownloadString($"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=gettemperature&ain={ain}");
            if (string.IsNullOrEmpty(result))
                return null;

            if (!double.TryParse(result, out var val))
                return null;

            return val;
        }

        /// <summary>
        /// Create an MD5 hash for our authentication token.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MD5 hash</returns>
        private string GetMD5Hash(string input)
        {
            var md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private async Task<string> DownloadString(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }
    }
}