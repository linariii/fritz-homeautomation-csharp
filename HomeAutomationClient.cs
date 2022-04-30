using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fritz.HomeAutomation.Extensions;
using Fritz.HomeAutomation.Models;
using System.Text;
using System.Threading.Tasks;
using Fritz.HomeAutomation.Enums;
using Fritz.HomeAutomation.Utils;

namespace Fritz.HomeAutomation
{
    /// <summary>
    /// Client
    /// </summary>
    public class HomeAutomationClient
    {
        private string _baseUrl;

        /// <summary>
        /// Get or set base url
        /// </summary>
        public string BaseUrl
        {
            get => _baseUrl;
            set => _baseUrl = value.TrimEnd('/');
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl">optional base url</param>
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
        public async Task<string> GetSessionId(string username, string password)
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
            if (sessionInfo.SessionId != "0000000000000000")
                return sessionInfo.SessionId;

            var response = sessionInfo.Challenge + "-" + HashUtils.GetMD5Hash(sessionInfo.Challenge + "-" + password);
            result = await DownloadString($"{BaseUrl}/login_sid.lua?username={username}&response={response}");
            sessionInfo = result.Deserialize<SessionInfo>();
            return sessionInfo?.SessionId;
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
        public async Task<State?> GetSwitchState(string sid, string ain)
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

            return !Enum.TryParse<State>(response, out var switchState)
                ? State.Unknown
                : switchState;
        }

        /// <summary>
        /// Turns on the outlet
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Ain</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<State?> SetSwitchOn(string sid, string ain)
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

            if (!Enum.TryParse<State>(response, out var switchState))
                return null;

            return switchState;
        }

        /// <summary>
        /// Determines connection status of the device
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Ain</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Present?> GetSwitchPresent(string sid, string ain)
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

            if (!Enum.TryParse<Present>(response, out var switchState))
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
        public async Task<State?> SetSwitchOff(string sid, string ain)
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

            if (!Enum.TryParse<State>(response, out var switchState))
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
        public async Task<State?> SetSwitchToggle(string sid, string ain)
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

            if (!Enum.TryParse<State>(response, out var switchState))
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
        public async Task<uint?> GetSwitchPower(string sid, string ain)
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

            if (!uint.TryParse(response, out var power))
                return null;

            return power;
        }

        /// <summary>
        /// Delivers the amount of energy energy taken from the outlet since initial start-up or reset of the energy statistics
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device Ain</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<uint?> GetSwitchEnergy(string sid, string ain)
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

            if (!uint.TryParse(response, out var power))
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
        public async Task<string> GetSwitchName(string sid, string ain)
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
        public async Task<uint?> GetTemperature(string sid, string ain)
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

            if (!uint.TryParse(response, out var val))
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
        public async Task<string> SetSimpleOnOff(string sid, string ain, SimpleOnOff state)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var val = (int)state;

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "setsimpleonoff", ain, $"onoff={val}"));
        }

        /// <summary>
        /// Get current target temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TemperatureResult> GetTargetTemperature(string sid, string ain)
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
            if (string.IsNullOrEmpty(response))
                return new TemperatureResult { State = ThermostatState.Unknown, Temperature = null };

            var temperature = TemperatureUtils.ApiToTemperature(response, out var state);
            return new TemperatureResult
            {
                State = state,
                Temperature = temperature
            };
        }

        /// <summary>
        /// Get current comfort( temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TemperatureResult> GetComfortTemperature(string sid, string ain)
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
            if (string.IsNullOrEmpty(response))
                return new TemperatureResult { State = ThermostatState.Unknown, Temperature = null };

            var temperature = TemperatureUtils.ApiToTemperature(response, out var state);
            return new TemperatureResult
            {
                State = state,
                Temperature = temperature
            };
        }

        /// <summary>
        /// Get current night temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TemperatureResult> GetEconomyTemperature(string sid, string ain)
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
            if (string.IsNullOrEmpty(response))
                return new TemperatureResult { State = ThermostatState.Unknown, Temperature = null };

            var temperature = TemperatureUtils.ApiToTemperature(response, out var state);
            return new TemperatureResult
            {
                State = state,
                Temperature = temperature
            };
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
        public async Task<DateTime?> SetThermostatBoost(string sid, string ain, int duration)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (duration < Constants.MinDurationInMinutes)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} musst be at least one minute", nameof(duration));

            if (duration > Constants.MaxDurationInMinutes)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} cannot exceed 24 hours", nameof(duration));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "sethkrboost", ain, $"endtimestamp={TimeUtils.TimeToApi(duration)}"));
            return TimeUtils.ApiToDatetime(response);
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
        public async Task<DateTime?> SetThermostatWindowOpen(string sid, string ain, int duration)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (duration < Constants.MinDurationInMinutes)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} musst be at least one minute", nameof(duration));

            if (duration > Constants.MaxDurationInMinutes)
                throw new ArgumentOutOfRangeException($"{nameof(duration)} cannot exceed 24 hours", nameof(duration));

            var response = await DownloadString(GetHomeAutoSwitchUrl(sid, "sethkrwindowopen", ain, $"endtimestamp={TimeUtils.TimeToApi(duration)}"));
            return TimeUtils.ApiToDatetime(response);
        }

        /// <summary>
        /// set current target temperature
        /// </summary>
        /// <param name="sid">Session ID</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="temperature"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> SetTargetTemperature(string sid, string ain, double temperature, ThermostatState state)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if (state == ThermostatState.Unknown)
                throw new ArgumentException($"{nameof(state)} cannot be unknown", nameof(ain));

            if (state == ThermostatState.Temperature && temperature < Constants.MinTemperature)
                throw new ArgumentException($"{nameof(temperature)} cannot be lower than {Constants.MinTemperature}°C", nameof(ain));

            if (state == ThermostatState.Temperature && temperature > Constants.MaxTemperature)
                throw new ArgumentException($"{nameof(temperature)} cannot be higher than {Constants.MaxTemperature}°C", nameof(ain));

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "sethkrtsoll", ain, $"param={TemperatureUtils.TemperatureToApi(temperature, state)}"));
        }

        /// <summary>
        /// Set level
        /// </summary>
        /// <param name="sid">Session id</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="level">Level (0 = 0%; 255 = 100%)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> SetLevel(string sid, string ain, byte level)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "setlevel", ain, $"level={level}"));
        }

        /// <summary>
        /// Set level percent
        /// </summary>
        /// <param name="sid">Session id</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="levelPercent">Level in percent (0 = 0%; 100 = 100%)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> SetLevelPercentage(string sid, string ain, byte levelPercent)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            if(levelPercent > 100)
                throw new ArgumentException($"{nameof(levelPercent)} cannot exceed 100%", nameof(levelPercent)) ;

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "setlevelpercentage", ain, $"level={levelPercent}"));
        }

        /// <summary>
        /// Set blind
        /// </summary>
        /// <param name="sid">Session id</param>
        /// <param name="ain">Device identifier</param>
        /// <param name="command">Command</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> SetBlind(string sid, string ain, BlindCommand command)
        {
            if (sid == null)
                throw new ArgumentNullException(nameof(sid));

            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentException($"{nameof(sid)} cannot be empty", nameof(sid));

            if (ain == null)
                throw new ArgumentNullException(nameof(ain));

            if (string.IsNullOrWhiteSpace(ain))
                throw new ArgumentException($"{nameof(ain)} cannot be empty", nameof(ain));

            var target = command.ToString().ToLowerInvariant();

            return await DownloadString(GetHomeAutoSwitchUrl(sid, "setblind", ain, $"target={target}"));
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