using Mendo.UAP.Common;
using MyShelf.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FriendsPage : PageBase
    {
        public FriendsPageViewModel ViewModel => FriendsPageViewModel.Instance;

        public FriendsPage()
        {
            this.InitializeComponent();
        }

        private void Instance_AuthStateChanged(object sender, API.Services.AuthState e)
        {
            if (IsCurrentPage)
                ViewModel.RefreshFriends();
        }

        protected override void LoadState(object parameter, Dictionary<string, object> pageState)
        {
            base.LoadState(parameter, pageState);

            API.Services.AuthenticationService.Instance.AuthStateChanged += Instance_AuthStateChanged;
            ViewModel.RefreshFriends();
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();

            API.Services.AuthenticationService.Instance.AuthStateChanged -= Instance_AuthStateChanged;
            base.SaveState(e, pageState);
        }

        private void StackPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}
