using Mendo.UAP.Common;
using MyShelf.API.Storage;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace MyShelf.Pages
{
    public sealed partial class SettingsPage : PageBase
    {



        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();

            base.SaveState(e, pageState);
        }

        private async void Rate_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9WZDNCRDR8RZ"));
        }

        private async void Feedback_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:feedback@fmendo.com?subject=MyShelf Feedback"));
        }

        private async void Privacy_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.goodreads.com/about/privacy"));
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!MyShelfSettings.Instance.DontShowAds)
            {
                try
                {
                    // The customer doesn't own this feature, so show the purchase dialog.
                    //await CurrentAppSimulator.RequestProductPurchaseAsync(MyShelfSettings.Instance.InAppProductKey, false);
                    await CurrentApp.RequestProductPurchaseAsync(MyShelfSettings.Instance.InAppProductKey, false);

                    MessageDialog m;
                    if (MyShelfSettings.Instance.DontShowAds)
                        m = new MessageDialog("Success! Ads have been removed.");
                    else
                        m = new MessageDialog("Could not complete purchase.");

                    m.ShowAsync();
                }
                catch (Exception)
                {
                    MessageDialog m = new MessageDialog("Could not complete purchase.");
                    m.ShowAsync();
                }
            }
            else
            {
                MessageDialog m = new MessageDialog("You have already purchased this.");
                m.ShowAsync();
            }
        }
    }
}
