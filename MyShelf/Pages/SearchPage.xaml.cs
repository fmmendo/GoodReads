using Mendo.UAP.Common;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : PageBase
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

            if (parameter != null)
            {
                if (parameter is string)
                {
                    ViewModel = new SearchPageViewModel();
                    ViewModel.SearchTerm = parameter as string;
                    ViewModel.SearchClick();
                }
                else if (parameter is SearchPageViewModel)
                    ViewModel = parameter as SearchPageViewModel;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
