using Mendo.UWP.Common;
using Mendo.UWP.Network;
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

        private async void ClearCache_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dialog = new MessageDialog("If you are experiencing issues or the app is displaying out-of-date information, please try clearing the cache.", "Clear Cache");

            var primary = new UICommand { Id = 1, Label = "Clear Cache" };
            dialog.Commands.Add(primary);

            dialog.CancelCommandIndex = 1;
            var secondary = new UICommand { Id = 2, Label = "Cancel" };
            dialog.Commands.Add(secondary);

            var result = await dialog.ShowAsync();

            if (result.Label.Equals("Clear Cache"))
            {                
                Http.DefaultCache.ClearCacheAsync().ContinueWith(clearCache =>
                {
                    if (clearCache.IsCompleted)
                    {
                        Dispatcher.RunAsync( Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            var md = new MessageDialog("Cache has been cleared. Try reloading the app if you were experiencing any issues.", "Success");
                            md.ShowAsync();
                        });
                    }
                    else
                    {
                        Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            var md = new MessageDialog("Sorry, something went wrong. Please try again later.", "Failed");
                            md.ShowAsync();
                        });
                    }
                });
            }
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
