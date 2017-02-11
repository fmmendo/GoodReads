using Mendo.UWP.Common;
using Microsoft.Advertising.WinRT.UI;
using MyShelf.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthorPage : BasePage
    {
        public AuthorViewModel ViewModel {get;set;}

        public bool ShowAds => !API.Storage.MyShelfSettings.Instance.DontShowAds;

        private const string TEST_APPLICATIONID = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
        private const string TEST_ADUNITID = "10865270";

        private const string WAPPLICATIONID = "9a9a9329-8a35-4262-8581-86f3042f9433";
        private const string WADUNITID = "335544";
        private const int AD_WIDTH = 300;
        private const int AD_HEIGHT = 250;

        private const string MAPPLICATIONID = "b5ffff7c-663f-4650-9377-de4b6b20fe7a";
        private const string MADUNITID = "335545";
        private const int AD_WIDTH_MOBILE = 480;
        private const int AD_HEIGHT_MOBILE = 80;

        private AdControl adControl = null;

        public AuthorPage()
        {
            this.InitializeComponent();

            SetUpAds();
        }

        private void SetUpAds()
        {
            // Initialize the AdControl.
            adControl = new AdControl();

            if (DeviceInformation.Instance.IsPhone)
            {
                adControl.ApplicationId = MAPPLICATIONID;
                adControl.AdUnitId = MADUNITID;
                adControl.Width = AD_WIDTH_MOBILE;
                adControl.Height = AD_HEIGHT_MOBILE;
            }
            else
            {
                adControl.ApplicationId = WAPPLICATIONID;
                adControl.AdUnitId = WADUNITID;
                adControl.Width = AD_WIDTH;
                adControl.Height = AD_HEIGHT;
            }
            adControl.IsAutoRefreshEnabled = true;

            adGrid.Children.Add(adControl);
        }

        protected override void LoadState(object parameter, Dictionary<string, object> pageState)
        {
            base.LoadState(parameter, pageState);

            if (parameter != null)
            {
                if (parameter is string)
                {
                    ViewModel = new AuthorViewModel(parameter as string);
                }
                else if (parameter is BookViewModel)
                    ViewModel = parameter as AuthorViewModel;
            }
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();

            base.SaveState(e, pageState);
        }
    }
}
