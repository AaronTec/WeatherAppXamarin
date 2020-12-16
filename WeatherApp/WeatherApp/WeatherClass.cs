using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace WeatherApp
{
    class WeatherClass
    {

        public class Rootobject // the Class for the API
        {
            public float lat { get; set; }
            public float lon { get; set; }
            public string timezone { get; set; }
            public int timezone_offset { get; set; }
            public Current current { get; set; }
            public Daily[] daily { get; set; }

            //public ObservableCollection<Daily> dailylist { get; set; }

            //public IList<Daily> daily = new List<Daily>()
        }

        public class Current
        {
            public int dt { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }

            public float _temp;

            public float temp // converting the tamp given into (Kelvin) into Celcius
            {
                set
                {
                    _temp = value;
                }
                get
                {
                    float tmp = _temp - 273.15f;
                    return (float)Math.Truncate(tmp);
                }
            }


            public float _feels_like;

            public float feels_like
            {
                set
                {
                    _feels_like = value;
                }
                get
                {
                    float fls = _feels_like - 273.15f;
                    return (float)Math.Truncate(fls);
                }
            }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public float dew_point { get; set; }
            public float uvi { get; set; }
            public int clouds { get; set; }
            public int visibility { get; set; }
            public float _wind_speed { get; set; }

            public float wind_speed// Converting m/s int kph
            {
                set
                {
                    _wind_speed = value;
                }
                get
                {
                    float fls = (_wind_speed * 18) / 5;
                    return (float)Math.Truncate(fls);
                }
            }

            public int wind_deg { get; set; }
            public Weather[] weather { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Daily
        {
            public int dt { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
            public Temp temp { get; set; }
            public Feels_Like feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public float dew_point { get; set; }
            public float _wind_speed { get; set; }

            public float wind_speed
            {
                set
                {
                    _wind_speed = value;
                }
                get
                {
                    float fls = (_wind_speed * 18) / 5;
                    return (float)Math.Truncate(fls);
                }
            }
            public int wind_deg { get; set; }
            public Weather1[] weather { get; set; }
            public int clouds { get; set; }
            public float pop { get; set; }
            public float uvi { get; set; }
            public float rain { get; set; }
        }

        public class Temp
        {
            public float _day;

            public float day
            {
                set
                {
                    _day = value;
                }
                get
                {
                    float tmp = _day - 273.15f;
                    return (float)Math.Truncate(tmp);
                }
            }

            public float _min { get; set; }
            public float min
            {
                set
                {
                    _min = value;
                }
                get
                {
                    float tmp = _min - 273.15f;
                    return (float)Math.Truncate(tmp);
                }
            }


            public float _max { get; set; }

            public float max
            {
                set
                {
                    _max = value;
                }
                get
                {
                    float tmp = _max - 273.15f;
                    return (float)Math.Truncate(tmp);
                }
            }

            public float night { get; set; }
            public float eve { get; set; }
            public float morn { get; set; }
        }

        public class Feels_Like
        {
            public float _day { get; set; }

            public float day
            {
                set
                {
                    _day = value;
                }
                get
                {
                    float tmp = _day - 273.15f;
                    return (float)Math.Truncate(tmp);
                }
            }

            public float night { get; set; }
            public float eve { get; set; }
            public float morn { get; set; }
        }

        public class Weather1
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

    }
}
