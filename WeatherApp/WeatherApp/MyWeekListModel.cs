using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.ObjectModel;

using System.Collections.ObjectModel;


namespace WeatherApp
{
    public class MyWeekListModel // Class for the List view in Weather Details.
    {



        List<string> thetemps = new List<string>();
        List<string> thetimes = new List<string>();
        List<int> theindexer = new List<int>();
        List<string> theimage = new List<string>();

        string[] coodinatesLat = { "-31.9505", "-33.8688", "-37.8136", "-35.2809", "-42.8821" };
        string[] coodinatesLong = { "115.8605", "151.2093", "144.9631", "149.1300", "147.3272" };

        string lati = "";
        string longy = "";


        public ObservableCollection<MyListModel> Dailylist { get; set; }

        public MyWeekListModel()
        {
            Dailylist = new ObservableCollection<MyListModel>();
            checkingprefs();
            retrivingdata();

            thismust();

        }

        public void retrivingdata() // Collecting the weather data from the API
        {

            string contents;

            using (var wc = new System.Net.WebClient())
                contents = wc.DownloadString("https://api.openweathermap.org/data/2.5/onecall?lat=" + lati + "&lon=" + longy + "&exclude=minutely,hourly,alerts&appid=1bad879db855abb9d741aa0a8d0ffcba");


            WeatherClass.Rootobject thedata = JsonConvert.DeserializeObject<WeatherClass.Rootobject>(contents);

            string thetime;
            float tmps;
            for (int i = 1; i < 8; i++)//Storing the weather data into its respectable list.
            {
                tmps = thedata.daily[i].temp.day;
                thetime = UnixTimeStampToDateTime((int)thedata.daily[i].dt).ToString("dddd") + "  " +UnixTimeStampToDateTime((int)thedata.daily[i].dt).ToString("m");
                thetemps.Add(tmps.ToString() + "º  " + thedata.daily[i].weather[0].description);
                thetimes.Add(thetime);
                theindexer.Add(i);
                theimage.Add("https://openweathermap.org/img/wn/" + thedata.daily[i].weather[0].icon + "@2x.png");
            }


        }

        public void thismust()// constructing the list view with the data
        {
            for (int i = 0; i < thetemps.Count; i++)
            {
                Dailylist.Add(new MyListModel { icons = theimage[i], tempreture = thetemps[i], dts = thetimes[i], indexer = theindexer[i]});
            }
        }

        private static DateTime UnixTimeStampToDateTime(int unixTimeStamp) //converting UNIX time to datetime
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void checkingprefs()// checking the current prefernces and making adjustsments accordingly.
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
        }
    }
}
