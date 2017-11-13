using Mendo.UWP;
using Mendo.UWP.Common;
using MyShelf.API.Services;
using MyShelf.API.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace MyShelf.ViewModels
{
    public class HomePageViewModel : BindableSingleton<HomePageViewModel>
    {
        private IAuthenticationService authService = AuthenticationService.Instance;
        private IUserService userService = UserService.Instance;
        
        public BindableCollection<UpdateViewModel> Updates { get; } = new BindableCollection<UpdateViewModel>();

        public HomePageViewModel()
        {
        }


        public async Task Refresh()
        {
            if (authService.State != AuthState.Authenticated)
            {
                authService.Authenticate();

                return;
            }
            
            RefreshUpdates();
        }

        public async Task RefreshUpdates()
        {
            Updates.Clear();
            Updates.LoadState = LoadState.Loading;

            var updates = await userService.GetFriendUpdates("", "", "100");
            foreach (var update in updates.Update)
                Updates.Add(new UpdateViewModel(update));

            Updates.LoadState = LoadState.Loaded;
        }
    }
}
