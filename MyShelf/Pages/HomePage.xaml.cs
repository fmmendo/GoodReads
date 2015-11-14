using Mendo.UAP.Common;
using Microsoft.AdMediator.Core.Models;
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
            WriteReviewControl.Hide();
        }

        private async void Instance_AuthStateChanged(object sender, API.Services.AuthState e)
        {
            if (IsCurrentPage)
                ViewModel.Refresh();
        }

        protected override async void LoadState(object parameter, Dictionary<string, object> pageState)
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

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var vm = (e.OriginalSource as Windows.UI.Xaml.Controls.HyperlinkButton)?.DataContext as UserStatusViewModel;
            if (vm == null)
                return;

            WriteReviewControl.Review = vm;
            WriteReviewControl.Show();
        }

        private void abbReading_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (lvReading.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                lvReading.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                lvReading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
