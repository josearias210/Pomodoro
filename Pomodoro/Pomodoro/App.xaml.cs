using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Pomodoro.Controls;
using Pomodoro.ViewModels;
using Pomodoro.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pomodoro
{
    public partial class App : PrismApplication
    {
        #region Constructs
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }
        #endregion

        #region Overrrite

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PomodoroNavigationPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<PomodoroPage, PomodoroPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfigurationPage, ConfigurationPageViewModel>();
            containerRegistry.RegisterForNavigation<PomodoroMasterDetailPage, PomodoroMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryPageViewModel>();
        }

        protected override void OnInitialized()
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("{licencia}");
            InitializeComponent();
            NavigationService.NavigateAsync(new Uri("/PomodoroMasterDetailPage/PomodoroNavigationPage/PomodoroPage", UriKind.Absolute));
        }

        protected override void OnStart()
        {
            //AppCenter.Start("android={android_token}", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion

    }
}