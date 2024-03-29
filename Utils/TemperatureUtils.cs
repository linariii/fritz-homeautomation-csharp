﻿using System;
using Fritz.HomeAutomation.Enums;

namespace Fritz.HomeAutomation.Utils
{
    /// <summary>
    /// Utils for temperature conversions
    /// </summary>
    public class TemperatureUtils
    {
        /// <summary>
        /// Convert temperature and state to temperature code
        /// </summary>
        /// <param name="temperature">temperature</param>
        /// <param name="state">state</param>
        /// <returns>temperatureCode</returns>
        public static int? TemperatureToApi(double temperature, ThermostatState state)
        {
            if (state == ThermostatState.On)
                return Constants.TemperatureOn;

            if (state == ThermostatState.Off)
                return Constants.TemperatureOff;

            return (int)Math.Round(Math.Min(Math.Max(temperature, Constants.MinTemperature), Constants.MaxTemperature), 0) * 2;
        }

        /// <summary>
        /// Convert temperature code to temperature and state
        /// </summary>
        /// <param name="temperatureCode">code</param>
        /// <param name="state">out state</param>
        /// <returns>temperature</returns>
        public static double? ApiToTemperature(string temperatureCode, out ThermostatState state)
        {
            state = ThermostatState.Unknown;
            if (string.IsNullOrWhiteSpace(temperatureCode))
                return null;

            if (!int.TryParse(temperatureCode, out var val))
                return null;

            if (val == Constants.TemperatureOn)
            {
                state = ThermostatState.On;
                return 0;
            }

            if (val == Constants.TemperatureOff)
            {
                state = ThermostatState.Off;
                return 0;
            }

            state = ThermostatState.Temperature;
            var number = (double)val;
            return number / 2;
        }
    }
}