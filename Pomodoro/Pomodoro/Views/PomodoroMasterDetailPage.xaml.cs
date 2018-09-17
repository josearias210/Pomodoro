using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pomodoro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PomodoroMasterDetailPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public PomodoroMasterDetailPage()
        {
            InitializeComponent();

        }

        public bool IsPresentedAfterNavigation { get { return false; } }

    }
}