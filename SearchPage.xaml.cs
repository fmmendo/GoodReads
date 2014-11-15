using GoodReads.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace GoodReads
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchPage : GoodReads.Common.LayoutAwarePage
    {
        GoodReads.API.GoodReads gr = new API.GoodReads();
        SearchViewModel _vm = new SearchViewModel();


        public SearchPage()
        {
            this.InitializeComponent();
            //this.DataContext = _vm;
            gr.Authenticate();

            //getdata();
        }

        private async void getdata()
        {
            var results = await gr.Search("tolkien");
            foreach (var item in results)
            {
                _vm.Titles.Add(item.BestBook.Title);
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            this.DefaultViewModel["Titles"] = _vm.Titles;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void pageTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //var tb = sender as TextBox;
            //if (tb != null)
            //{
            //    //_vm.Titles = new List<string>();
            //    var results = await gr.Search(tb.Text);
            //    foreach (var item in results)
            //    {
            //        _vm.Titles.Add(item.BestBook.Title);
            //    }

            gr.GetFriendUpdates("", "", "15");
            //}
        }
    }
}
