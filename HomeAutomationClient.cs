using System;
using System.Collections.Generic;
using System.Linq;
using Fritz.HomeAutomation.Extensions;
using Fritz.HomeAutomation.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fritz.HomeAutomation
{
    public class HomeAutomationClient
    {
        private string _baseUrl;

        public string BaseUrl
        {
            get => _baseUrl;
            set => _baseUrl = value.TrimEnd('/');
        }

        public HomeAutomationClient(string baseUrl = "http://fritz.box")
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

            var result = await DownloadString($"{BaseUrl}/login_sid.lua?username={username}");
            if (string.IsNullOrEmpty(result))
                return null;

            var sessionInfo = result.Deserialize<SessionInfo>();
            if (sessionInfo.Sid != "0000000000000000")
                return sessionInfo.Sid;

            var response = sessionInfo.Challenge + "-" + GetMD5Hash(sessionInfo.Challenge + "-" + password);
            result = await DownloadString($"{BaseUrl}/login_sid.lua?username={username}&response={response}");
            sessionInfo = result.Deserialize<SessionInfo>();
            return sessionInfo?.Sid;

        }

        /// <summary>
        /// Get all available switches
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <returns></returns>
        public async Task<string[]> GetSwitchList(string sid)
        {
            if (string.IsNullOrEmpty(sid))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchlist");
            return string.IsNullOrEmpty(response)
                ? null
                : response.Split(',');
        }

        /// <summary>
        /// List all available devices 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <returns></returns>
        public async Task<List<Device>> GetDevices(string sid)
        {
            if (string.IsNullOrEmpty(sid))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getdevicelistinfos");
            var result = string.IsNullOrEmpty(response)
                ? null
                : response.Deserialize<DeviceList>();

            return result?.Devices;
        }

        /// <summary>
        /// List all available devices 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        public async Task<List<Device>> GetFilteredDevices(string sid, Functions filter)
        {
            if (string.IsNullOrEmpty(sid))
                return null;

            var devices = await GetDevices(sid);
            return devices.Where(d => d.Functions.HasValue && d.Functions.Value.HasFlag(filter)).ToList();
        }


        /// <summary>
        /// Determines switching state of the outlet
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns>0 = deactivated, 1 = activated</returns>
        public async Task<int?> GetSwitchState(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchstate&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Turns on the outlet
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<int?> SetSwitchOn(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchon&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Determines connection status of the device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<int?> GetSwitchPresent(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchpresent&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Turns off the outlet
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<int?> SetSwitchOff(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchoff&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Toggle the state of the selected device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<int?> SetSwitchToggle(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=setswitchtoggle&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Delivers the amount of energy currently taken from the outlet 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<int?> GetSwitchPower(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchpower&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var power))
                return null;

            return power;
        }

        /// <summary>
        /// Delivers the amount of energy energy taken from the outlet since initial start-up or reset of the energy statistics
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Identifier</param>
        /// <returns></returns>
        public async Task<int?> GetSwitchEnergy(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchpower&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!int.TryParse(response, out var power))
                return null;

            return power;
        }

        /// <summary>
        /// Returns name of the device 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<string> GetSwitchnNme(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            return await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getswitchname&ain={ain}");

        }

        /// <summary>
        /// Get current temperature of the selected device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<double?> GetTemperature(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=gettemperature&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            if (!double.TryParse(response, out var val))
                return null;

            return val;
        }

        /// <summary>
        /// Provides the basic statistics (temperature,voltage, power, energy) of the device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<Device> GetDeviceInfos(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getdeviceinfos&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            return string.IsNullOrEmpty(response)
                ? null
                : response.Deserialize<Device>();

        }

        /// <summary>
        /// Provides the basic statistics (temperature,voltage, power, energy) of the device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<DeviceStats> GetBasicDeviceStats(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=getbasicdevicestats&ain={ain}");
            if (string.IsNullOrEmpty(response))
                return null;

            return string.IsNullOrEmpty(response)
                ? null
                : response.Deserialize<DeviceStats>();

        }

        /// <summary>
        /// switch the device on, oFf or toggle its current state
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="state">0 = off, 1 = on, 2 = toggle</param>
        /// <returns></returns>
        public async Task<string> SetSimpleOnOff(string sid, string ain, int state)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;
            
            //toggle state if invalid state if used
            if (state < 0 || state > 2)
                state = 2;

            return await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=setsimpleonoff&ain={ain}&onoff={state}");
        }

        /// <summary>
        /// Get current target temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<string> GetTempTarget(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=gethkrtsoll&ain={ain}");
            return string.IsNullOrEmpty(response)
                ? null
                : ApiToTemp(response);
        }

        /// <summary>
        /// Get current comfort( temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<string> GetTempComfort(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=gethkrkomfort&ain={ain}");
            return string.IsNullOrEmpty(response)
                ? null
                : ApiToTemp(response);
        }

        /// <summary>
        /// Get current night temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        public async Task<string> GetTempNigh(string sid, string ain)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=gethkrabsenk&ain={ain}");
            return string.IsNullOrEmpty(response)
                ? null
                : ApiToTemp(response);
        }

        /// <summary>
        /// activate boost with end time or deactivate boost
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="duration">in minutes (max. 24h)</param>
        /// <returns></returns>
        public async Task<DateTime?> SetHkrBoost(string sid, string ain, int duration)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=sethkrboost&ain={ain}&endtimestamp={TimeToApi(duration)}");
            return ApiToDatetime(response);
        }

        /// <summary>
        /// activate window open with end time or deactivate 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="duration">in minutes (max. 24h)</param>
        /// <returns></returns>
        public async Task<DateTime?> SetHkrWindowOpen(string sid, string ain, int duration)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=sethkrwindowopen&ain={ain}&endtimestamp={TimeToApi(duration)}");
            return ApiToDatetime(response);
        }

        /// <summary>
        /// set current target temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        public async Task<string> SetTempTarget(string sid, string ain, string temperature)
        {
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(ain))
                return null;

            var response = await DownloadString($"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd=sethkrtsoll&ain={ain}&param={TempToApi(temperature)}");
            return string.IsNullOrEmpty(response)
                ? null
                : ApiToTemp(response);
        }

        private DateTime? ApiToDatetime(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
                return null;

            if (!long.TryParse(time, out var val))
                return null;

            return DateTimeOffset.FromUnixTimeSeconds(val).LocalDateTime;
        }

        private long TimeToApi(int minutes)
        {
            if (minutes <= 0)
                return 0;

            if (minutes > 1440) //24h
                minutes = 1440;

            var dateTime = DateTime.Now.AddMinutes(minutes);

            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        private int? TempToApi(string temperature)
        {
            const int minTemp = 8;
            const int maxTemp = 28;

            if (string.IsNullOrWhiteSpace(temperature))
                return null;

            if (temperature == "on")
                return 254;

            if (temperature == "off")
                return 253;

            if (!double.TryParse(temperature.Replace(" °C", ""), out var val))
                return null;

            return (int)Math.Round(Math.Min(Math.Max(val, minTemp), maxTemp), 0) * 2;

        }

        private string ApiToTemp(string temperature)
        {
            if (string.IsNullOrWhiteSpace(temperature))
                return null;

            if (!int.TryParse(temperature, out var val))
                return null;

            if (val == 254)
                return "on";

            if (val == 253)
                return "off";

            var number = (double)val;
            return $"{number / 2} °C";
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