using Prism.Mvvm;

namespace Pomodoro.ViewModels
{
    public class MenuItemViewModel : BindableBase
    {
        #region Properties
        public string Code { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        #endregion

        #region Constructs
        public MenuItemViewModel(string title, string code, string icon,string url)
        {
            Code = code;
            Icon = icon;
            Title = title;
            Url = url;
        }
        #endregion

    }
}
