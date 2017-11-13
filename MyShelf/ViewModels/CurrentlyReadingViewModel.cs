using Mendo.UWP;
using Mendo.UWP.Common;
using MyShelf.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class CurrentlyReadingViewModel : BindableSingleton<CurrentlyReadingViewModel>
    {
        private IAuthenticationService authService = AuthenticationService.Instance;
        private IUserService userService = UserService.Instance;

        public BindableCollection<UserStatusViewModel> CurrentlyReading { get; } = new BindableCollection<UserStatusViewModel>();

        public CurrentlyReadingViewModel()
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
            //RefreshCurrentlyReading();
            RefreshCurrentlyReading();
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
    }
}
