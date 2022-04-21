//https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/AHA-HTTP-Interface.pdf

using Fritz.HomeAutomation.Extensions;
using Fritz.HomeAutomation.Models;
using System.Security.Cryptography;
using System.Text;

namespace Fritz.HomeAutomation
{
    public class HomeAutomationClient
    {
        public string BaseUrl { get; set; }

        public HomeAutomationClient(string baseUrl = "http://fritz.box/")
        {
            BaseUrl = baseUrl;
        }

        public string Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            return GetSid(username, password);
        }

        private string GetSid(string username, string password)
        {
            var url = $"{BaseUrl}login_sid.lua?username={username}";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            var sessionInfo = result.Deserialize<SessionInfo>();
            if (sessionInfo.SID == "0000000000000000")
            {
                var response = sessionInfo.Challenge + "-" + GetMD5Hash(sessionInfo.Challenge + "-" + password);
                url = $"{BaseUrl}login_sid.lua?username={username}&response={response}";
                result = DownloadString(url);
                sessionInfo = result.Deserialize<SessionInfo>();
                if (sessionInfo == null)
                    return null;

                return sessionInfo.SID;
            }
            else
            {
                return sessionInfo.SID;
            }
        }

        public Devicelist GetDevices(string sid)
        {
            if (string.IsNullOrEmpty(sid))
                return null;

            var url = $"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=getdevicelistinfos";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            return result.Deserialize<Devicelist>();
        }

        public int? GetSwitchState(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var url = $"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchstate&ain={ain}";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var switchState))
                return null;

            return switchState;
        }

        public int? SetSwitchOn(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var url = $"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchon&ain={ain}";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var switchState))
                return null;

            return switchState;
        }

        public int? SetSwitchOff(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var url = $"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchoff&ain={ain}";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            if (!int.TryParse(result, out var switchState))
                return null;

            return switchState;
        }

        public double? GetSwitchPower(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var url = $"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchpower&ain={ain}";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            if (!double.TryParse(result, out var power))
                return null;

            return power;
        }

        public double? GetTemperature(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var url = $"{BaseUrl}webservices/homeautoswitch.lua?sid={sid}&switchcmd=gettemperature&ain={ain}";
            var result = DownloadString(url);
            if (string.IsNullOrEmpty(result))
                return null;

            if (!double.TryParse(result, out var val))
                return null;

            return val;
        }

        private string GetMD5Hash(string input)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private string DownloadString(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}

