using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string[] coodinatesLat = { "-31.9505", "-33.8688", "-37.8136", "-35.2809", "-42.8821" };// storing the latitude of the selectable destinations
        string[] coodinatesLong = { "115.8605", "151.2093", "144.9631", "149.1300", "147.3272" };//Perth, Sydney, Melbourne, Canberra and Hobart. IN ORDER

        string lati = "";
        string longy = "";

        string location;


        public MainPage()
        {
            InitializeComponent();
            checkingprefs();
            retrivingdata();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            checkingprefs();
            retrivingdata();
        }

        public async void retrivingdata()// retrieving the data from the API and displaying the data to the Labels.
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync("https://api.openweathermap.org/data/2.5/onecall?lat="+ lati + "&lon=" + longy + "&exclude=minutely,hourly,alerts&appid=1bad879db855abb9d741aa0a8d0ffcba");

                var responseString = await response.Content.ReadAsStringAsync();

                WeatherClass.Rootobject thedata = JsonConvert.DeserializeObject<WeatherClass.Rootobject>(responseString);


                lbl_date.Text = UnixTimeStampToDateTime(thedata.current.dt).ToShortDateString();

                lbl_theday.Text = location;

                lbl_Temp.Text = thedata.current.temp.ToString() + "º";

                lbl_feelsLike.Text = thedata.current.wind_speed.ToString()+ "km/hr";

                lbl_humididty.Text = thedata.current.humidity.ToString() + "%";

                lbl_UV.Text = thedata.current.uvi.ToString()+ "UV";

                lbl_lowtemp.Text = thedata.current.weather[0].description.ToString();

                iconim.Source = "https://openweathermap.org/img/wn/" + thedata.current.weather[0].icon + "@2x.png";

            }
            catch
            {
                await DisplayAlert("ERROR", "No internet, retard", "OK");
            }
        }

        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp)// Converting the UNIX Timestamp to Date time
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            //var shortDateValue = dtDateTime.ToShortDateString();
            return dtDateTime;
        }

        private async void btn_nextday(object sender, EventArgs e)// Weekday details display.
        {
            WeekdaysDisplay contactPage = new WeekdaysDisplay();
            await Navigation.PushModalAsync(contactPage);

            if (Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                if ((bool)Application.Current.Properties["SoundEffects"])
                {
                    SoundEffect.PlayButtonTick();
                }
            }

        }

        private async void btn_setting(object sender, EventArgs e)// Button for Prefereances.
        {
            PrefrencePage contactPage = new PrefrencePage();
            await Navigation.PushModalAsync(contactPage);

            if (Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                if ((bool)Application.Current.Properties["SoundEffects"])
                {
                    SoundEffect.PlayButtonTick();
                }
            }

        }

        private void checkingprefs()//Checking the current preferences and making adjustments accordingly.
        {
            if (!Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                Application.Current.Properties["SoundEffects"] = false;
            }


            if (Application.Current.Properties.ContainsKey("NightMode"))
            {
                bool night = (bool)Application.Current.Properties["NightMode"];


                if (night)
                {
                    mainBg.BackgroundColor = Color.FromHex("000000");
                    mainim.Opacity = 0.2f;
                    lbl_date.TextColor = System.Drawing.Color.White;
                    //btn_col.BackgroundColor = System.Drawing.Color.Gray;
                    lbl_theday.TextColor = System.Drawing.Color.White;
                    lbl_Temp.TextColor = System.Drawing.Color.White;
                    lbl_humididty.TextColor = System.Drawing.Color.White;
                    lbl_UV.TextColor = System.Drawing.Color.White;
                    lbl_lowtemp.TextColor = System.Drawing.Color.White;
                    lbl_feelsLike.TextColor = System.Drawing.Color.White;
                }
                else
                {
                    mainBg.BackgroundColor = Color.FromHex("000000");
                    mainim.Opacity = 0.8f;
                    //backcolMain.BackgroundColor = Color.FromHex("7FB3D5");
                    lbl_date.TextColor = System.Drawing.Color.Black;
                    //btn_col.BackgroundColor = System.Drawing.Color.Black;
                    lbl_theday.TextColor = System.Drawing.Color.Black;
                    lbl_Temp.TextColor = System.Drawing.Color.Black;
                    lbl_humididty.TextColor = System.Drawing.Color.Black;
                    lbl_UV.TextColor = System.Drawing.Color.Black;
                    lbl_lowtemp.TextColor = System.Drawing.Color.Black;
                    lbl_feelsLike.TextColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                Application.Current.Properties["NightMode"] = false;
            }



            //else if ((bool)Application.Current.Properties.ContainsKey("NightMode"))
            //{
            //    backcolMain.BackgroundColor = Color.FromHex("000000");
            //    lbl_date.TextColor = System.Drawing.Color.White;
            //    btn_col.BackgroundColor = System.Drawing.Color.Gray;
            //    lbl_theday.TextColor = System.Drawing.Color.White;
            //    lbl_Temp.TextColor = System.Drawing.Color.White;
            //    lbl_disHumidity.TextColor = System.Drawing.Color.White;
            //    lbl_humididty.TextColor = System.Drawing.Color.White;
            //    lbl_disUV.TextColor = System.Drawing.Color.White;
            //    lbl_UV.TextColor = System.Drawing.Color.White;
            //    lbl_description.TextColor = System.Drawing.Color.White;
            //    lbl_lowtemp.TextColor = System.Drawing.Color.White;
            //    lbl_disFeelsLike.TextColor = System.Drawing.Color.White;
            //    lbl_feelsLike.TextColor = System.Drawing.Color.White;
            //    lbl_disHumidity.TextColor = System.Drawing.Color.White;


            //}
            //else if(!(bool)Application.Current.Properties.ContainsKey("NightMode"))
            //{
            //    backcolMain.BackgroundColor = Color.FromHex("FFFFFF");
            //    lbl_date.TextColor = System.Drawing.Color.Black;
            //    btn_col.BackgroundColor = System.Drawing.Color.Black;
            //    lbl_theday.TextColor = System.Drawing.Color.Black;
            //    lbl_Temp.TextColor = System.Drawing.Color.Black;
            //    lbl_disHumidity.TextColor = System.Drawing.Color.Black;
            //    lbl_humididty.TextColor = System.Drawing.Color.Black;
            //    lbl_disUV.TextColor = System.Drawing.Color.Black;
            //    lbl_UV.TextColor = System.Drawing.Color.Black;
            //    lbl_description.TextColor = System.Drawing.Color.Black;
            //    lbl_lowtemp.TextColor = System.Drawing.Color.Black;
            //    lbl_disFeelsLike.TextColor = System.Drawing.Color.Black;
            //    lbl_feelsLike.TextColor = System.Drawing.Color.Black;
            //    lbl_disHumidity.TextColor = System.Drawing.Color.Black;
            //}

            if (!Application.Current.Properties.ContainsKey("city_select"))// checks Dict what city is selected.
            {
                Application.Current.Properties["city_select"] = 0;
            }
            else
            {
                int inx = (int)Application.Current.Properties["city_select"];
                lati = coodinatesLat[inx];
                longy = coodinatesLong[inx];


                if(inx == 0)
                {
                    location = "Perth";
                }
                if (inx == 1)
                {
                    location = "Sydney";
                }
                if (inx == 2)
                {
                    location = "Melbourne";
                }
                if (inx == 3)
                {
                    location = "Canberra";
                }
                if (inx == 4)
                {
                    location = "Hobart";
                }



            }

            //bool night = (bool)Application.Current.Properties.ContainsKey("NightMode"));


        }


    }
}
