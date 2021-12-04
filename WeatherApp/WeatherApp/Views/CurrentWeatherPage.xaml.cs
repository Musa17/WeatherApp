using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Helpers;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentWeatherPage : ContentPage
    {
        private string Location;
        private string APIkey;

        public CurrentWeatherPage()
        {
            InitializeComponent();
            GetWeatherInfo();
        }

        private async void GetWeatherInfo()
        {
            Location = "Split";

            var data = await new DatabaseService().GetKeysAsync();

            foreach (var item in data)
            {
                APIkey = item.APIkey;
            }

            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid={APIkey}&units=metric";

            var result = await APIcaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }
    }
}