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
    public class FriendsPageViewModel : SingletonViewModelBase<FriendsPageViewModel>
    {
        private IUserService userService = UserService.Instance;
        private IAuthenticationService authService = AuthenticationService.Instance;

        public ObservableCollection<UserViewModel> Friends { get; set; } = new ObservableCollection<UserViewModel>();

        public async Task RefreshFriends()
        {
            if (authService.State != AuthState.Authenticated)
            {
                authService.Authenticate();

                return;
            }

            Friends.Clear();

            var result = await userService.GetFriends();

            foreach (var friend in result.User)
                Friends.Add(new UserViewModel(friend));
        }
    }
}
