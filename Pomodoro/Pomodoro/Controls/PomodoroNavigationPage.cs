using Pomodoro.Events;
using Prism.Events;
using Xamarin.Forms;

namespace Pomodoro.Controls
{
    public class PomodoroNavigationPage : NavigationPage
    {
        #region Injects
        IEventAggregator _eventAggregator;
        #endregion

        #region Contructs
        public PomodoroNavigationPage(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

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
                }
                else
                {
                    this.BarBackgroundColor = Color.Red;
                }
            });
        }
        #endregion
    }
}
