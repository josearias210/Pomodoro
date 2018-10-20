using Pomodoro.Interfaces;
using Pomodoro.Resources;
using Xamarin.Forms;

namespace Pomodoro.Helpers
{
    public static class Languages
    {
        #region Constructs
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        #endregion

        #region Properties

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string RestartHistory
        {
            get { return Resource.RestartHistory; }
        }

        public static string TimePomodoro
        {
            get { return Resource.TimePomodoro; }
        }

        public static string ShortBreaks
        {
            get { return Resource.ShortBreaks; }
        }

        public static string LongBreaks
        {
            get { return Resource.LongBreaks; }
        }

        public static string NumberPomodoro
        {
            get { return Resource.NumberPomodoro; }
        }

        public static string Save
        {
            get { return Resource.Save; }
        }

        public static string Configurations
        {
            get { return Resource.Configurations; }
        }

        public static string History
        {
            get { return Resource.History; }
        }

        public static string Day
        {
            get { return Resource.Day; }
        }

        public static string SummaryDay
        {
            get { return Resource.SummaryDay; }
        }

        public static string DurationMin
        {
            get { return Resource.DurationMin; }
        }

        public static string Pomodoros
        {
            get { return Resource.Pomodoros; }
        }

        public static string Breaks
        {
            get { return Resource.Breaks; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string Pomodoro
        {
            get { return Resource.Pomodoro; }
        }

        public static string StartPause
        {
            get { return Resource.StartPause; }
        }

        public static string ShortBreakSet
        {
            get { return Resource.ShortBreakSet; }
        }

        public static string LongBreakSet
        {
            get { return Resource.LongBreakSet; }
        }
        public static string Yes
        {
            get { return Resource.Yes; }
        }

        public static string No
        {
            get { return Resource.No; }
        }

        public static string QuestionDeleteHistory
        {
            get { return Resource.QuestionDeleteHistory; }
        }

        public static string ErrorDeleteHistory
        {
            get { return Resource.ErrorDeleteHistory; }
        }

        public static string SuccessDeleteHistory
        {
            get { return Resource.SuccessDeleteHistory; }
        }

        public static string ErrorSaveconfiguration
        {
            get { return Resource.ErrorSaveConfiguration; }
        }

        public static string SuccessSaveConfiguration
        {
            get { return Resource.SuccessSaveConfiguration; }
        }

        public static string TakeABreak
        {
            get { return Resource.TakeABreak; }
        }

        public static string BackToWork
        {
            get { return Resource.BackToWork; }
        }

        public static string LetsGo
        {
            get { return Resource.LetsGo; }
        }

        public static string LongBreak
        {
            get { return Resource.LongBreak; }
        }

        public static string ShortBreak
        {
            get { return Resource.ShortBreak; }
        }
        /*
	
LongBreak	You can now have a long break...	
ShortBreak	You can now have a short break...	

         */
        #endregion
    }
}
