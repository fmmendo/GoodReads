using Mendo.UAP.Common;
using MyShelf.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : PageBase
    {
        HomePageViewModel ViewModel => HomePageViewModel.Instance;

        public HomePage()
        {
            InitializeComponent();
        }

        private void Instance_AuthStateChanged(object sender, API.Services.AuthState e)
        {
            if (IsCurrentPage)
                ViewModel.Refresh();
        }

        protected override void LoadState(object parameter, Dictionary<string, object> pageState)
        {
            base.LoadState(parameter, pageState);

            API.Services.AuthenticationService.Instance.AuthStateChanged += Instance_AuthStateChanged;
            ViewModel.Refresh();
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();
            API.Services.AuthenticationService.Instance.AuthStateChanged -= Instance_AuthStateChanged;

            base.SaveState(e, pageState);
        }
    }
}
