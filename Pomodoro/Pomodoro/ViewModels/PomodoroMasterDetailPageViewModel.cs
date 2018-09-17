using Pomodoro.Helpers;
using Pomodoro.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;

namespace Pomodoro.ViewModels
{
    public class PomodoroMasterDetailPageViewModel: BindableBase
    {
        #region Attributes
        private MenuItemViewModel selectedItem;
        private ObservableRangeCollection<MenuItemViewModel> menuItems;
        #endregion

        #region Properties
        public ObservableRangeCollection<MenuItemViewModel> MenuItems
        {
            get { return menuItems; }
            set { SetProperty(ref menuItems, value); }
        }

        public MenuItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                if (selectedItem != null && !string.IsNullOrEmpty(selectedItem.Url))
                {
                    GoCommand.Execute();
                }
            }
        }
        #endregion

        #region Commands
        public DelegateCommand GoCommand { get; set; }
        #endregion

        #region Injects
        private INavigationService navigationService;
        #endregion

        #region Constructors
        public PomodoroMasterDetailPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            GoCommand = new DelegateCommand(Go);
            MenuItems = new ObservableRangeCollection<MenuItemViewModel>();
            LoadMenu();
        }

        private async void Go()
        {
            //await navigationService.NavigateAsync(new Uri(SelectedItem.Url,UriKind.Absolute));
            await navigationService.NavigateAsync(new Uri(selectedItem.Url, UriKind.Relative));
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            var options = new List<MenuItemViewModel>() {
                new MenuItemViewModel(Languages.Pomodoro, Languages.Pomodoro, "ic_timer.png", "PomodoroNavigationPage/PomodoroPage"),
                new MenuItemViewModel(Languages.History, Languages.History, "ic_report.png", "PomodoroNavigationPage/HistoryPage"),
                new MenuItemViewModel(Languages.Configurations, Languages.Configurations, "ic_config.png", "PomodoroNavigationPage/ConfigurationPage") };

            MenuItems.AddRange(options);
        }
        #endregion

    }
}
