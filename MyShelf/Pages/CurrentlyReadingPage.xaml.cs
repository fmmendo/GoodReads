using Mendo.UWP.Common;
using MyShelf.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CurrentlyReadingPage : PageBase
    {

        CurrentlyReadingViewModel ViewModel => CurrentlyReadingViewModel.Instance;

        public CurrentlyReadingPage()
        {
            this.InitializeComponent();
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
            var vm = (e.OriginalSource as HyperlinkButton)?.DataContext as UserStatusViewModel;
            if (vm == null)
                return;

            WriteReviewControl.Review = vm;
            WriteReviewControl.Show();
        }
    }
}
