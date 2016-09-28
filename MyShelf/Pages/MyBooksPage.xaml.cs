using Mendo.UWP.Common;
using MyShelf.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyBooksPage : BasePage
    {
        MyBooksPageViewModel ViewModel => MyBooksPageViewModel.Instance;

        public MyBooksPage()
        {
            InitializeComponent();
        }

        private void Instance_AuthStateChanged(object sender, API.Services.AuthState e)
        {
            if (IsCurrentPage)
                ViewModel.GetUserShelves();
        }

        protected override void LoadState(object parameter, Dictionary<string, object> pageState)
        {
            base.LoadState(parameter, pageState);

            API.Services.AuthenticationService.Instance.AuthStateChanged += Instance_AuthStateChanged;
            ViewModel.GetUserShelves();
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();

            API.Services.AuthenticationService.Instance.AuthStateChanged -= Instance_AuthStateChanged;
            base.SaveState(e, pageState);
        }
    }
}
