using Pomodoro.Events;
using Pomodoro.Services;
using Prism.Events;
using Xamarin.Forms;

namespace Pomodoro.Controls
{
    public class PomodoroNavigationPage : NavigationPage
    {
        #region Injects
        IEventAggregator _eventAggregator;
        IStatusBarColorService _statusBarColorService;
        #endregion

        #region Contructs
        public PomodoroNavigationPage(IEventAggregator eventAggregator, IStatusBarColorService statusBarColorService)
        {
            _eventAggregator = eventAggregator;
            _statusBarColorService = statusBarColorService;

        }
        #endregion

        #region Overrite
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _eventAggregator.GetEvent<UpdateNavBarEvent>().Subscribe(UpdateColor);
            _eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _eventAggregator.GetEvent<UpdateNavBarEvent>().Unsubscribe(UpdateColor);
        }
        #endregion

        #region Methods
        void UpdateColor(bool isWorking)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (isWorking)
                {
                    this.BarBackgroundColor = Color.DarkGreen;
                    _statusBarColorService.ChangeColor(Color.DarkGreen);
                }
                else
                {
                    this.BarBackgroundColor = Color.Red;
                    _statusBarColorService.ChangeColor(Color.Red);
                }
            });
        }
        #endregion
    }
}
