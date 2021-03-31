using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace GenericHostConsoleApp
{
    internal sealed class WeatherService : IWeatherService
    {
        private readonly IOptions<WeatherSettings> _weatherSettings;

        public WeatherService(IOptions<WeatherSettings> weatherSettings)
        {
            _weatherSettings = weatherSettings;
        }

        public Task<IReadOnlyList<int>> GetFiveDayTemperaturesAsync()
        {
            var temperatures = new[] { 76, 76, 77, 79, 78 };
            if (!_weatherSettings.Value.Unit.Equals("C", StringComparison.OrdinalIgnoreCase))
                return Task.FromResult<IReadOnlyList<int>>(temperatures);
            for (var i = 0; i < temperatures.Length; i++)
            {
                temperatures[i] = (int)Math.Round((temperatures[i] - 32) / 1.8);
            }

            return Task.FromResult<IReadOnlyList<int>>(temperatures);
        }
    }
}