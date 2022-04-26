using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetSid(string username, string password)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"{nameof(username)} cannot be empty", nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"{nameof(password)} cannot be empty", nameof(password));

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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string[]> GetSwitchList(string sid)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getswitchlist"));
            return string.IsNullOrEmpty(response)
                ? null
                : response.Split(',');
        }

        /// <summary>
        /// List all available devices 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Device>> GetDevices(string sid)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getdevicelistinfos"));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Device>> GetFilteredDevices(string sid, Functions filter)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            var devices = await GetDevices(sid);
            return devices?.Where(d => d.Functions.HasValue && d.Functions.Value.HasFlag(filter)).ToList();
        }


        /// <summary>
        /// Determines switching state of the outlet
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns>0 = deactivated, 1 = activated</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> GetSwitchState(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getswitchstate", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> SetSwitchOn(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "setswitchon", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> GetSwitchPresent(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getswitchpresent", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> SetSwitchOff(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "setswitchoff", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> SetSwitchToggle(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "setswitchtoggle", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> GetSwitchPower(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getswitchpower", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int?> GetSwitchEnergy(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getswitchenergy", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetSwitchnName(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "getswitchname", ain));

        }

        /// <summary>
        /// Get current temperature of the selected device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<double?> GetTemperature(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "gettemperature", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Device> GetDeviceInfos(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getdeviceinfos", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<DeviceStats> GetBasicDeviceStats(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "getbasicdevicestats", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<string> SetSimpleOnOff(string sid, string ain, int state)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (state < 0)
                throw new ArgumentOutOfRangeException($"{nameof(state)} musst be 0=off, 1=on or 2=toggle", nameof(state));

            if (state > 2)
                throw new ArgumentOutOfRangeException($"{nameof(state)} musst be 0=off, 1=on or 2=toggle", nameof(state));

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "setsimpleonoff", ain, $"onoff={state}"));
        }

        /// <summary>
        /// Get current target temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetTempTarget(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "gethkrtsoll", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetTempComfort(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "gethkrkomfort", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetTempNigh(string sid, string ain)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "gethkrabsenk", ain));
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<DateTime?> SetHkrBoost(string sid, string ain, int duration)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (duration <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} musst be at least one minute", nameof(duration));

            if (duration > 14440)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} cannot exceed 24 hours", nameof(duration));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "sethkrboost", ain, $"endtimestamp={TimeToApi(duration)}"));
            return ApiToDatetime(response);
        }

        /// <summary>
        /// activate window open with end time or deactivate 
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="duration">in minutes (max. 24h)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<DateTime?> SetHkrWindowOpen(string sid, string ain, int duration)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (duration <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} musst be at least one minute", nameof(duration));

            if (duration > 14440)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} cannot exceed 24 hours", nameof(duration));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "sethkrwindowopen", ain, $"endtimestamp={TimeToApi(duration)}"));
            return ApiToDatetime(response);
        }

        /// <summary>
        /// set current target temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> SetTempTarget(string sid, string ain, string temperature)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (temperature == null)
                throw new ArgumentNullException(nameof(temperature));

            if (string.IsNullOrWhiteSpace(temperature))
                throw new ArgumentException($"{nameof(temperature)} cannot be empty", nameof(temperature));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "sethkrtsoll", ain, $"param={TempToApi(temperature)}"));
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
            using (var client = new WebClient { Encoding = Encoding.UTF8 })
            {
                return await client.DownloadStringTaskAsync(url);
            }
        }

        private string GetHomeAutoSwitchUrl(string sid, string command)
        {
            return $"{BaseUrl}/webservices/homeautoswitch.lua?sid={sid}&switchcmd={command}";
        }

        private string GetHomeAutoSwitchUrl(string sid, string command, string ain)
        {
            return $"{GetHomeAutoSwitchUrl(sid, command)}&ain={ain}";
        }

        private string GetHomeAutoSwitchUrl(string sid, string command, string ain, string parameter)
        {
            return $"{GetHomeAutoSwitchUrl(sid, command, ain)}&{parameter}";
        }
    }
}