using Mendo.UWP.Common;
using MyShelf.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml;
using System.Linq;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Advertising.WinRT.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookPage : BasePage
    {
        public BookViewModel ViewModel { get; set; }
        public bool ShowAds => !API.Storage.MyShelfSettings.Instance.DontShowAds;

        private const int AD_WIDTH = 728;
        private const int AD_HEIGHT = 90;
        //private const int AD_WIDTH = 480;
        //private const int AD_HEIGHT = 80;
        private const string WAPPLICATIONID = "<tbd>";
        private const string WADUNITID = "<tbd>";
        private const string MAPPLICATIONID = "<tbd>";
        private const string MADUNITID = "<tbd>";

        private AdControl adControl = null;
        private string myAppId = WAPPLICATIONID;
        private string myAdUnitId = WADUNITID;

        public BookPage()
        {
            this.InitializeComponent();

            VisualStateManager.GoToState(this, HiddenState.Name, true);

            if (DeviceInformation.Instance.IsPhone)
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

                myAppId = MAPPLICATIONID;
                myAdUnitId = MADUNITID;
            }

            // Initialize the AdControl.
            adControl = new AdControl();
            adControl.ApplicationId = myAppId;
            adControl.AdUnitId = myAdUnitId;
            adControl.Width = AD_WIDTH;
            adControl.Height = AD_HEIGHT;
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
                    ViewModel = new BookViewModel(parameter as string);
                }
                else if (parameter is BookViewModel)
                    ViewModel = parameter as BookViewModel;
            }
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();

            base.SaveState(e, pageState);
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (DisplayStates.CurrentState == VisibleState)
                VisualStateManager.GoToState(this, HiddenState.Name, true);
        }

        private void GridView_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            ViewModel.SelectedReview = e.ClickedItem as ReviewViewModel;

            if (DisplayStates.CurrentState == HiddenState)
                VisualStateManager.GoToState(this, VisibleState.Name, true);
        }

        private void rect_Tapped(object sender, TappedRoutedEventArgs e)
        {
                VisualStateManager.GoToState(this, HiddenState.Name, true);
        }

        private void GridView_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            ViewModel.SelectedReview = e.AddedItems.FirstOrDefault() as ReviewViewModel;

            if (DisplayStates.CurrentState == HiddenState)
                VisualStateManager.GoToState(this, VisibleState.Name, true);
        }
    }
}
