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

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!MyShelfSettings.Instance.DontShowAds)
            {
                try
                {
                    // The customer doesn't own this feature, so show the purchase dialog.
                    await CurrentAppSimulator.RequestProductPurchaseAsync(MyShelfSettings.Instance.InAppProductKey, false);
                    //await CurrentApp.RequestProductPurchaseAsync(MyShelfSettings.Instance.InAppProductKey, false);

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




            //var licenseInformation = CurrentAppSimulator.LicenseInformation;

            //// get the license info for this app from the simulator
            //ListingInformation listing = await CurrentAppSimulator.LoadListingInformationAsync();

            //await CurrentAppSimulator.RequestProductPurchaseAsync(MyShelfSettings.Instance.InAppProductKey, false);

            //// get the ProductListing object for the product named "product1"
            //ProductListing thisProduct = listing.ProductListings[MyShelfSettings.Instance.InAppProductKey];

            //// format the purchase string or this in-app offer
            //String purchasePrice = "You can buy " + thisProduct.Name +
            //    " for: " + thisProduct.FormattedPrice + ".";




            //// get all in-app products for current app
            ////ListingInformation allProducts = await CurrentApp.LoadListingInformationByProductIdsAsync(new string[0]);
            //ListingInformation products = await CurrentApp.LoadListingInformationByProductIdsAsync(new string[] { MyShelfSettings.Instance.InAppProductKey });
            //var vals = products.ProductListings.Values;
            //await CurrentApp.RequestProductPurchaseAsync(MyShelfSettings.Instance.InAppProductKey, false);
            //// get specific in-app product by ID
            //ProductListing productListing = null;
            //if (!products.ProductListings.TryGetValue(MyShelfSettings.Instance.InAppProductKey, out productListing))
            //{
            //    MessageDialog m = new MessageDialog("Could not find product information");
            //    m.ShowAsync();

            //    return;
            //}

            //// start product purchase
            //await CurrentApp.RequestProductPurchaseAsync(productListing.ProductId, false);

            //ProductLicense productLicense = null;
            //if (CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(MyShelfSettings.Instance.InAppProductKey, out productLicense))
            //{
            //    if (productLicense.IsActive)
            //    {
            //        MessageDialog m = new MessageDialog("Product purchased");
            //        m.ShowAsync();

            //        return;
            //    }
            //}

            //MessageDialog md = new MessageDialog("Product not purchased");
            //md.ShowAsync();

        }
    }
}
