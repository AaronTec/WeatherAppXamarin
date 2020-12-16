using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;


namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Day0 : ContentPage
    {
        public Day0()
        {
            InitializeComponent();

            retrivingdata();
        }

        public async void retrivingdata()
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync("https://api.openweathermap.org/data/2.5/onecall?lat=31.9505&lon=115.8605&exclude=minutely,hourly,alerts&appid=1bad879db855abb9d741aa0a8d0ffcba");

                var responseString = await response.Content.ReadAsStringAsync();

                WeatherClass.Rootobject thedata = JsonConvert.DeserializeObject<WeatherClass.Rootobject>(responseString);


                lbl_date.Text = UnixTimeStampToDateTime(thedata.daily[1].dt).ToShortDateString();

                lbl_theday.Text = UnixTimeStampToDateTime(thedata.daily[1].dt).ToString("dddd");

                lbl_Temp.Text = thedata.daily[1].temp.day.ToString() + "º";

                lbl_maxtemp.Text = thedata.daily[1].temp.max.ToString() + "º";

                lbl_mintemp.Text = thedata.daily[1].temp.min.ToString() + "º";

                lbl_feelsLike.Text = thedata.daily[1].feels_like.day.ToString() + "º";

                lbl_humididty.Text = thedata.daily[1].humidity.ToString();

                lbl_UV.Text = thedata.daily[1].uvi.ToString();

                lbl_lowtemp.Text = thedata.current.weather[1].description.ToString();

            }
            catch
            {
                await DisplayAlert("ERROR", "No internet", "OK");
            }
        }

        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            //var shortDateValue = dtDateTime.ToShortDateString();
            return dtDateTime;
        }

    }
}