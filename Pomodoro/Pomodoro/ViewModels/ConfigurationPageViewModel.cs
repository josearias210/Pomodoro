using Microsoft.AppCenter.Crashes;
using Pomodoro.Events;
using Pomodoro.Helpers;
using Pomodoro.Models;
using Pomodoro.Repositories;
using Pomodoro.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;

namespace Pomodoro.ViewModels
{
    public class ConfigurationPageViewModel : BindableBase
    {
        #region Attributes
        private short selectedWorking;
        private short selectedPomodoros;
        private short selectedLongBreaks;
        private short selectedShortBreaks;
        private ObservableRangeCollection<short> working;
        private ObservableRangeCollection<short> pomodoros;
        private ObservableRangeCollection<short> longBreaks;
        private ObservableRangeCollection<short> shortBreaks;
        #endregion

        #region Properties
        public ObservableRangeCollection<short> Working
        {
            get { return working; }
            set { working = value; SetProperty(ref working, value); }
        }

        public short SelectedWorking
        {
            get { return selectedWorking; }
            set { selectedWorking = value; SetProperty(ref selectedWorking, value); }
        }

        public ObservableRangeCollection<short> ShortBreaks
        {
            get { return shortBreaks; }
            set { shortBreaks = value; SetProperty(ref shortBreaks, value); }
        }

        public short SelectedShortBreaks
        {
            get { return selectedShortBreaks; }
            set { selectedShortBreaks = value; SetProperty(ref selectedShortBreaks, value); }
        }

        public ObservableRangeCollection<short> LongBreaks
        {
            get { return longBreaks; }
            set { longBreaks = value; SetProperty(ref longBreaks, value); }
        }

        public short SelectedLongBreaks
        {
            get { return selectedLongBreaks; }
            set { selectedLongBreaks = value; SetProperty(ref selectedLongBreaks, value); }
        }

        public ObservableRangeCollection<short> Pomodoros
        {
            get { return pomodoros; }
            set { pomodoros = value; SetProperty(ref pomodoros, value); }
        }

        public short SelectedPomodoros
        {
            get { return selectedPomodoros; }
            set { selectedPomodoros = value; SetProperty(ref selectedPomodoros, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ResetCommand { get; set; }
        #endregion

        #region Injects
        private INavigationService navigationService;
        private IPageDialogService dialogService;
        #endregion

        #region Constructs
        public ConfigurationPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator)
        {
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            Working = new ObservableRangeCollection<short>();
            ShortBreaks = new ObservableRangeCollection<short>();
            LongBreaks = new ObservableRangeCollection<short>();
            Pomodoros = new ObservableRangeCollection<short>();

            SaveCommand = new DelegateCommand(Save);
            ResetCommand = new DelegateCommand(Reset);
            eventAggregator.GetEvent<UpdateNavBarEvent>().Publish(false);
            LoadControls();
            LoadConfiguracion();
        }
        #endregion

        #region Methods
        private async void Reset()
        {
            var answer = await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.QuestionDeleteHistory, Languages.Yes, Languages.No);
            if (answer)
            {
                var repo = new HistoryRepository();
                try
                {
                    repo.DeleteStores();
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.ErrorDeleteHistory, Languages.Accept);
                    return;
                }

                await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.SuccessDeleteHistory, Languages.Accept);

            }
        }
        private void LoadControls()
        {
            Working.AddRange(new short[] { 5, 10, 15, 20, 25, 30, 45, 60 });
            ShortBreaks.AddRange(new short[] { 5, 10, 15, 20, 25, 30, 45, 60 });
            LongBreaks.AddRange(new short[] { 5, 10, 15, 20, 25, 30, 45, 60 });
            Pomodoros.AddRange(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }
        private void LoadConfiguracion()
        {
            var repo = new ConfigurationRepository();
            var config = repo.LoadAsync();
            if (config == null) {
                config = new Configuration()
                {
                    Working = 25,
                    ShortBreak = 5,
                    LongBreak = 20,
                    Pomorodos = 4
                };
            }

            SelectedWorking = config.Working;
            SelectedShortBreaks = config.ShortBreak;
            SelectedLongBreaks = config.LongBreak;
            SelectedPomodoros = config.Pomorodos;
        }
        private async void Save()
        {
            var repo = new ConfigurationRepository();
            var configuracion = new Configuration()
            {
                Working = SelectedWorking,
                ShortBreak = SelectedShortBreaks,
                LongBreak = SelectedLongBreaks,
                Pomorodos = SelectedPomodoros
            };

            try
            {
                repo.SaveAsync(configuracion);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.ErrorSaveconfiguration, Languages.Accept);
                return;
            }

            await dialogService.DisplayAlertAsync(Languages.Pomodoro, Languages.SuccessSaveConfiguration, Languages.Accept);

        }
        #endregion
    }
}
