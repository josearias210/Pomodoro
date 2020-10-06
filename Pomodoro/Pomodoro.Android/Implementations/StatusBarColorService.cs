using Android.OS;
using Plugin.CurrentActivity;
using Pomodoro.Droid.Implementations;
using Pomodoro.Services;
using Xamarin.Forms.Platform.Android;

namespace Pomodoro.Droid.Implementations
{
    public class StatusBarColorService : IStatusBarColorService
    {
        public void ChangeColor(Xamarin.Forms.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            { 
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(color.ToAndroid());
            }
        }
    }
}
