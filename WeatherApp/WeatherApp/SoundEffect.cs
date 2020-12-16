using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp
{
    class SoundEffect
    {
        static Plugin.SimpleAudioPlayer.ISimpleAudioPlayer tick = null;
        public static void PlayButtonTick()
        {
            if (tick==null)
            {
                tick = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                tick.Load("tap.wav");
            }
            tick.Play();

        }
        

    }
}
