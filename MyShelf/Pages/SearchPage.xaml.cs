using Mendo.UWP.Common;
using MyShelf.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : BasePage
    {
        public SearchPageViewModel ViewModel { get; set; }

        public SearchPage()
        {
            this.InitializeComponent();
            ViewModel = new SearchPageViewModel();
        }

        protected override void LoadState(object parameter, Dictionary<string, object> pageState)
        {
            base.LoadState(parameter, pageState);

            ConnectedAnimation searchAniamtion = ConnectedAnimationService.GetForCurrentView().GetAnimation("Search");
            if (searchAniamtion != null)
            {
                searchAniamtion.TryStart(Search);
            }

            if (parameter != null)
            {
                if (parameter is string query)
                {
                    Search.Text = query;
                    ViewModel = new SearchPageViewModel();
                    ViewModel.SearchTerm = query;
                    ViewModel.SearchClick();
                }
                else if (parameter is SearchPageViewModel)
                    ViewModel = parameter as SearchPageViewModel;
            }
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();

            base.SaveState(e, pageState);
        }

        private void Search_QuerySubmitted(Windows.UI.Xaml.Controls.AutoSuggestBox sender, Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.SearchTerm = args.QueryText;
            ViewModel.SearchClick();
        }
    }
}
