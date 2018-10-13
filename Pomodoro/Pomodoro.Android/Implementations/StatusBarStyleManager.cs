using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pomodoro.Droid.Implementations;
using Xamarin.Forms;
using Pomodoro.Interfaces;

[assembly: Dependency(typeof(StatusBarStyleManager))]
namespace Pomodoro.Droid.Implementations
{
    class StatusBarStyleManager : IStatusBarStyleManager
    {
        public void SetColor(Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = 0;
                    
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Rgb(
                        GetIntColor(color.R), GetIntColor(color.G), GetIntColor(color.B)));
                });
            }
        }

        private int GetIntColor(double color)
        {
            return (int) Math.Truncate(color * 255);
        }

        Window GetCurrentWindow()
        {
            var window = ((Activity)Forms.Context).Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }
    }
}
