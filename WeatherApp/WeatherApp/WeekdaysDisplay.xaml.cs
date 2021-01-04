using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeekdaysDisplay : ContentPage
    {
        int index = 1;

        string[] coodinatesLat = { "-31.9505", "-33.8688", "-37.8136", "-35.2809", "-42.8821" };
        string[] coodinatesLong = { "115.8605", "151.2093", "144.9631", "149.1300", "147.3272" };

        string lati = "";
        string longy = "";

        public WeekdaysDisplay()
        {
            InitializeComponent();

            checkingprefs();

            retrivingdata();

            BindingContext = new MyWeekListModel();
            
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {

            if (Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                if ((bool)Application.Current.Properties["SoundEffects"])
                {
                    SoundEffect.PlayButtonTick();
                }
            }

            var mydetails = e.Item as MyListModel;

            index = mydetails.indexer;

            retrivingdata();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            checkingprefs();
            retrivingdata();

            BindingContext = new MyWeekListModel();
        }

        public async void retrivingdata()
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync("https://api.openweathermap.org/data/2.5/onecall?lat=" + lati + "&lon=" + longy + "&exclude=minutely,hourly,alerts&appid=[REDACTED]");

                var responseString = await response.Content.ReadAsStringAsync();

                WeatherClass.Rootobject thedata = JsonConvert.DeserializeObject<WeatherClass.Rootobject>(responseString);


                lbl_date.Text = UnixTimeStampToDateTime(thedata.daily[index].dt).ToShortDateString();

                lbl_theday.Text = UnixTimeStampToDateTime(thedata.daily[index].dt).ToString("dddd");

                lbl_Temp.Text = thedata.daily[index].temp.day.ToString() + "º";

                lbl_feelsLike.Text = thedata.daily[index].wind_speed.ToString()+ "km/hr";

                lbl_humididty.Text = thedata.daily[index].humidity.ToString()+"%";

                lbl_UV.Text = thedata.daily[index].uvi.ToString() + "UV";

                lbl_lowtemp.Text = thedata.daily[index].weather[0].description.ToString();

                iconindi.Source = "https://openweathermap.org/img/wn/"+ thedata.daily[index].weather[0].icon +"@2x.png";

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

        private async void btn_back(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                if ((bool)Application.Current.Properties["SoundEffects"])
                {
                    SoundEffect.PlayButtonTick();
                }
            }


            await Navigation.PopModalAsync();

        }

        private void checkingprefs()
        {
            if (!Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                Application.Current.Properties["SoundEffects"] = false;
            }


            if (!Application.Current.Properties.ContainsKey("NightMode"))
            {
                Application.Current.Properties["NightMode"] = false;
            }

            if (!Application.Current.Properties.ContainsKey("city_select"))
            {
                Application.Current.Properties["city_select"] = 0;
            }
            else
            {
                int inx = (int)Application.Current.Properties["city_select"];
                lati = coodinatesLat[inx];
                longy = coodinatesLong[inx];
            }

            if (Application.Current.Properties.ContainsKey("NightMode"))
            {
                bool night = (bool)Application.Current.Properties["NightMode"];


                if (night)
                {
                    mainBg.BackgroundColor = Color.FromHex("000000");
                    mainim.Opacity = 0.2f;
                    //lst_view.BackgroundColor = Color.FromHex("1A5276");
                    lbl_date.TextColor = System.Drawing.Color.White;
                    lbl_theday.TextColor = System.Drawing.Color.White;
                    lbl_Temp.TextColor = System.Drawing.Color.White;
                    lbl_humididty.TextColor = System.Drawing.Color.White;
                    lbl_feelsLike.TextColor = System.Drawing.Color.White;
                    lbl_UV.TextColor = System.Drawing.Color.White;
                    lbl_lowtemp.TextColor = System.Drawing.Color.White;
                    lbl_feelsLike.TextColor = System.Drawing.Color.White;
                    lbl_restof.TextColor = System.Drawing.Color.White;
                }
                else
                {
                    mainBg.BackgroundColor = Color.FromHex("000000");
                    mainim.Opacity = 0.8f;
                    //lst_view.BackgroundColor = Color.FromHex("5499C7");
                    lbl_feelsLike.TextColor = System.Drawing.Color.Black;
                    lbl_date.TextColor = System.Drawing.Color.Black;
                    lbl_theday.TextColor = System.Drawing.Color.Black;
                    lbl_Temp.TextColor = System.Drawing.Color.Black;
                    lbl_humididty.TextColor = System.Drawing.Color.Black;
                    lbl_UV.TextColor = System.Drawing.Color.Black;
                    lbl_restof.TextColor = System.Drawing.Color.Black;
                    lbl_lowtemp.TextColor = System.Drawing.Color.Black;
                    lbl_feelsLike.TextColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                Application.Current.Properties["NightMode"] = false;
            }




        }

    }
}
