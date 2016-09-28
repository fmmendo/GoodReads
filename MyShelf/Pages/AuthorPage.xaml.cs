using Mendo.UWP.Common;
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

        public AuthorPage()
        {
            this.InitializeComponent();
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
