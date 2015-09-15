using Mendo.UAP.Common;
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
    public class HomePageViewModel : SingletonViewModelBase<HomePageViewModel>
    {
        private IAuthenticationService authService = AuthenticationService.Instance;
        private IUserService userService = UserService.Instance;

        private ProductLicense productLicense = null;

        public ObservableCollection<UpdateViewModel> Updates { get; } = new ObservableCollection<UpdateViewModel>();
        public ObservableCollection<UserStatusViewModel> CurrentlyReading { get; } = new ObservableCollection<UserStatusViewModel>();

        private bool showCurrentlyReading = false;
        public bool ShowCurrentlyReading
        {
            get { return showCurrentlyReading; }
            set {showCurrentlyReading = value; OnPropertyChanged(); }
        }

        public bool CanShowAds => !(productLicense != null && productLicense.IsActive);

        public HomePageViewModel()
        {
            CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(MyShelfSettings.Instance.InAppProductKey, out productLicense);
        }


        public async Task Refresh()
        {
            if (authService.State != AuthState.Authenticated)
            {
                authService.Authenticate();

                return;
            }

            // if currently reading is 2nd it will only load after 
            // all books from updates have finished loading...
            RefreshCurrentlyReading();
            RefreshUpdates();
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
                    CurrentlyReading.Add(new UserStatusViewModel(status));
                }
            }
        }

        public void CurrentlyReadingClick()
        {
            ShowCurrentlyReading = true;
        }
    }
}
