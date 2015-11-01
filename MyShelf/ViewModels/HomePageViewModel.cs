using Mendo.UAP;
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
        
        public DynamicCollection<UpdateViewModel> Updates { get; } = new DynamicCollection<UpdateViewModel>();
        public DynamicCollection<UserStatusViewModel> CurrentlyReading { get; } = new DynamicCollection<UserStatusViewModel>();

        private bool showCurrentlyReading = false;
        public bool ShowCurrentlyReading
        {
            get { return showCurrentlyReading; }
            set {showCurrentlyReading = value; OnPropertyChanged(); }
        }


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

            // if currently reading is 2nd it will only load after 
            // all books from updates have finished loading...
            RefreshCurrentlyReading();
            RefreshUpdates();
        }

        public async Task RefreshUpdates()
        {
            Updates.Clear();
            Updates.LoadState = LoadState.Loading;
            var updates = await userService.GetFriendUpdates("", "", "");
            foreach (var update in updates.Update)
                Updates.Add(new UpdateViewModel(update));

            Updates.LoadState = LoadState.Loaded;
        }

        public async Task RefreshCurrentlyReading()
        {
            CurrentlyReading.LoadState = LoadState.Loading;
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

                //TODO: this might give me crap later
                foreach (var update in user.Updates.Update.Where(u => u.Type.Equals("readstatus")))
                {
                    var id = update?.Object?.ReadStatus?.Review?.Book?.Id;
                    if ((update.ActionText.StartsWith("started reading") || update.ActionText.StartsWith("is currently reading"))
                        && !String.IsNullOrEmpty(id)
                        && !CurrentlyReading.Select(cr => cr.BookId).Contains(update?.Object?.ReadStatus?.Review?.Book?.Id)
                        && !user.Updates.Update.Where(u => u.Type.Equals("review")).Any(r => r.Object.Book.Id.Equals(id)))

                        CurrentlyReading.Add(new UserStatusViewModel(update));
                }

                CurrentlyReading.LoadState = LoadState.Loaded;
            }
            else
            {
                CurrentlyReading.SetFaulted();
            }
        }

        public void CurrentlyReadingClick()
        {
            ShowCurrentlyReading = true;
        }
    }
}
