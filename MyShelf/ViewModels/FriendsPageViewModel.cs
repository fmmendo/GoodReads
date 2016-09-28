using Mendo.UWP.Common;
using MyShelf.API.Services;
using MyShelf.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class FriendsPageViewModel : BindableSingleton<FriendsPageViewModel>
    {
        private IUserService userService = UserService.Instance;
        private IAuthenticationService authService = AuthenticationService.Instance;

        public ObservableCollection<UserViewModel> Friends { get; set; } = new ObservableCollection<UserViewModel>();

        public bool IsLoading { get { return GetV(false); } set { Set(value); } }

        private UserViewModel selectedFriend;
        public UserViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                selectedFriend = value;
                OnPropertyChanged();

                NavigationService.Navigate(typeof(UserPage), selectedFriend.Id);
            }
        }


        public async Task RefreshFriends()
        {
            IsLoading = true;
            if (authService.State != AuthState.Authenticated)
            {
                authService.Authenticate();

                return;
            }

            Friends.Clear();

            var result = await userService.GetFriends();

            foreach (var friend in result.User)
                Friends.Add(new UserViewModel(friend));

            IsLoading = false;
        }
    }
}
