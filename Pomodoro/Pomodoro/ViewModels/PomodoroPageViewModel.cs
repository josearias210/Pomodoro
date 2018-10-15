using Pomodoro.Classes;
using Pomodoro.Events;
using Pomodoro.Models;
using Pomodoro.Repositories;
using Pomodoro.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Timers;
using Plugin.LocalNotifications;

namespace Pomodoro.ViewModels
{
    public class PomodoroPageViewModel : BindableBase, IDestructible
    {
        #region Attributes
        private Timer timer;
        private TimeSpan elapsed;
        private int pomodoros = 0;
        private bool isRunning;
        private bool isWorking;
        private Activity currentActivity;
        private Configuration config;
        private int durations;
        #endregion

        #region Properties
        public int Durations
        {
            get { return durations; }
            set { SetProperty(ref durations, value); }
        }

        public TimeSpan Elapsed
        {
            get { return elapsed; }
            set { SetProperty(ref elapsed, value); }
        }

        private Activity CurrentActivity
        {
            get { return currentActivity; }
            set
            {
                IsWorking = value == Activity.Working;
                eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(IsWorking);
                currentActivity = value;
                soundService.ChangeActivity();
            }
        }


        public bool IsWorking
        {
            get { return isWorking; }
            set { SetProperty(ref isWorking, value); }
        }


        public bool IsRunning
        {
            get { return isRunning; }
            set { SetProperty(ref isRunning, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand StartStopCommand { get; set; }
        public DelegateCommand<string> ChangeStateCommand { get; set; }
        #endregion

        #region Services
        private SoundService soundService;
        #endregion

        #region Injects
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructors
        public PomodoroPageViewModel(IEventAggregator eventAggregator)
        {
            
            this.soundService = new SoundService();
            this.eventAggregator = eventAggregator;
            StartStopCommand = new DelegateCommand(StartStop);
            ChangeStateCommand = new DelegateCommand<string>(ChangeState);
            Loadconfiguracion();
            ConfigureTimer();
            
        }
        #endregion

        #region Methods
        private void ChangeState(string activityId)
        {
            ChangeState((Activity)int.Parse(activityId));
        }

        public void ChangeState(Activity activity)
        {

            if (activity == Activity.LongBreak)
            {
                Durations = config.LongBreak * 60;
                pomodoros = 0;
            }
            else if (activity == Activity.ShortBreak)
            {
                Durations = config.ShortBreak * 60;
            }
            else
            {
                Durations = config.Working * 60;
            }

            CurrentActivity = activity;
            Elapsed = new TimeSpan(0, 0, 0);
        }

        private void Loadconfiguracion()
        {
            CurrentActivity = Activity.Working;
            var repo = new ConfigurationRepository();
            config = repo.LoadAsync();
            Elapsed = new TimeSpan(0, 0, 0);
            Durations = config.Working * 60;
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Elapsed = Elapsed.Add(TimeSpan.FromSeconds(1));

            if (CurrentActivity == Activity.Working && Elapsed.TotalSeconds >= config.Working * 60)
            {
                AddHistory(Elapsed.TotalMinutes, true);
                pomodoros++;
                if (pomodoros == config.Pomorodos)
                {
                    ChangeState(Activity.LongBreak);
                    
                    CrossLocalNotifications.Current.Show(Resources.Resource.TimePomodoro, "You can now have a long break...");
                }
                else
                {
                    ChangeState(Activity.ShortBreak);
                    CrossLocalNotifications.Current.Show("Take a break!", "You can now have a short break...");
                }
            }

            if ((CurrentActivity == Activity.ShortBreak && Elapsed.TotalSeconds >= config.ShortBreak * 60)
               || (CurrentActivity == Activity.LongBreak && Elapsed.TotalSeconds >= config.LongBreak * 60))
            {
                AddHistory(Elapsed.TotalMinutes, false);
                ChangeState(Activity.Working);
                CrossLocalNotifications.Current.Show("Let´s go", "Time to go back to work!");
            }

        }


        private void AddHistory(double durations, bool isWorking)
        {
            var repo = new HistoryRepository();
            var entity = new History()
            {
                Durations = durations,
                End = DateTime.Now.Date,
                IsWorking = isWorking
            };
            repo.SaveAsync(entity);
        }

        private void ConfigureTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Start()
        {

            timer.Start();
            IsRunning = true;
        }

        private void Stop()
        {

            timer.Stop();
            IsRunning = false;
        }

        private void StartStop()
        {
            if (IsRunning)
            {
                Stop();

            }
            else
            {
                Start();
            }
        }
        public void Destroy()
        {
            try
            {
                timer.Stop();
                timer = null;
            }
            catch { }
        }
        #endregion
    }
}