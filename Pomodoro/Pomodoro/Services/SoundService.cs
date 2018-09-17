using Microsoft.AppCenter.Crashes;
using Plugin.SimpleAudioPlayer;
using System;

namespace Pomodoro.Services
{
    public class SoundService
    {
        #region Methods
        public void ChangeActivity()
        {
            try
            {
                ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
                player.Load("status.mp3");
                player.Play();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}
