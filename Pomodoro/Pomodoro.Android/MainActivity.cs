
using Android.App;
using Android.Content.PM;
using Android.OS;
using Pomodoro.Droid.Implementations;
using Pomodoro.Services;
using Prism;
using Prism.Ioc;

namespace Pomodoro.Droid
{
    [Activity(Label = "Pomodoro", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterInstance<IStatusBarColorService>(new StatusBarColorService());
            }
        }

    }
}

