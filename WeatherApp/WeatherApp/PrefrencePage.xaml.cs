using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrefrencePage : ContentPage
    {
        public PrefrencePage()
        {
            InitializeComponent();


            thechecks();

            sw_nightmode.IsToggled = (bool)Application.Current.Properties["NightMode"];

            sw_sound.IsToggled = (bool)Application.Current.Properties["SoundEffects"];
        }

        private void thechecks()
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
                Application.Current.Properties["city_select"] = 1;
            }




        }

        private void sw_sound_Toggled(object sender, ToggledEventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("SoundEffects"))
            {

                if (!sw_sound.IsToggled)
                {
                    Application.Current.Properties["SoundEffects"] = false;


                }
                else
                {
                    Application.Current.Properties["SoundEffects"] = true;

                }
            }

        }

        async void sw_nightmode_Toggled(object sender, ToggledEventArgs e)
        {

            if (Application.Current.Properties.ContainsKey("NightMode"))
            {    

                if (!sw_nightmode.IsToggled)
                {

                    Application.Current.Properties["NightMode"] = false;


                    SettingsBg.BackgroundColor = Color.FromHex("FFFFFF");
                    preferencestitle.TextColor = System.Drawing.Color.Black;
                    l2.TextColor = System.Drawing.Color.Black;
                    l3.TextColor = System.Drawing.Color.Black;
                    l4.TextColor = System.Drawing.Color.Black;
                    //Backbut.TextColor = System.Drawing.Color.White;
                    sw_nightmode.BackgroundColor = System.Drawing.Color.White;
                    sw_sound.BackgroundColor = System.Drawing.Color.White;
                    //Backbut.BackgroundColor = System.Drawing.Color.Gray;

                }
                else
                {

                    Application.Current.Properties["NightMode"] = true;


                    SettingsBg.BackgroundColor = Color.FromHex("000000");
                    preferencestitle.TextColor = System.Drawing.Color.White;
                    l2.TextColor = System.Drawing.Color.White;
                    l3.TextColor = System.Drawing.Color.White;
                    l4.TextColor = System.Drawing.Color.White;
                    //Backbut.TextColor = System.Drawing.Color.White;
                    sw_nightmode.BackgroundColor = System.Drawing.Color.Gray;
                    sw_sound.BackgroundColor = System.Drawing.Color.Gray;
                    //Backbut.BackgroundColor = System.Drawing.Color.Gray;
                }
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string selectedcity = string.Empty;

            try
            {
                selectedcity = picked.Items[picked.SelectedIndex];
            }
            catch
            {
                selectedcity = "Perth";
            }


            if (selectedcity == "Perth")
            {
                Application.Current.Properties["city_select"] = 0;
            }
            else if (selectedcity == "Sydney")
            {
                Application.Current.Properties["city_select"] = 1;
            }
            else if (selectedcity == "Melbourne")
            {
                Application.Current.Properties["city_select"] = 2;
            }
            else if (selectedcity == "Canberra")
            {
                Application.Current.Properties["city_select"] = 3;
            }
            else if (selectedcity == "Hobart")
            {
                Application.Current.Properties["city_select"] = 4;
            }



            if (Application.Current.Properties.ContainsKey("SoundEffects"))
            {
                if ((bool)Application.Current.Properties["SoundEffects"])
                {
                    SoundEffect.PlayButtonTick();
                }
            }

            await Navigation.PopModalAsync();
        }
    }
}