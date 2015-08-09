using Mendo.UAP.Common;
using MyShelf.API.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class HomePageViewModel : SingletonViewModelBase<HomePageViewModel>
    {
        private IAuthenticationService authService = AuthenticationService.Instance;
        private IUserService userService = UserService.Instance;

        public ObservableCollection<UpdateViewModel> Updates { get; } = new ObservableCollection<UpdateViewModel>();
        public ObservableCollection<string> CurrentlyReading { get; } = new ObservableCollection<string>();




        public HomePageViewModel()
        {
            authService.Authenticate();
        }

        public async Task RefreshUpdates()
        {
            Updates.Clear();

            var updates = await userService.GetFriendUpdates("", "", "");
            foreach (var update in updates.Update)
                Updates.Add(new UpdateViewModel(update));
        }

        public async Task RefreshCurrentlyReading()
        {
            if (!userService.IsUserIdAvailable)
                await userService.GetUserID();

            if (userService.IsUserIdAvailable)
            {
                var user = await userService.GetUserInfo();

                CurrentlyReading.Clear();

                foreach (var status in user.UserStatuses)
                {
                    CurrentlyReading.Add(status.Book.Title);
                }
            }
        }
    }
}
