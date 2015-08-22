using Mendo.UAP.Common;
using MyShelf.API.Services;
using MyShelf.API.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class MainFrameViewModel : SingletonViewModelBase<MainFrameViewModel>
    {
        IUserService userService = UserService.Instance;

        #region Hamburger Menu States
        private bool? _isHomeChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsHomeChecked
        {
            get { return _isHomeChecked; }
            set { _isHomeChecked = value; OnPropertyChanged(); }
        }

        private bool? _isMyBooksChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsMyBooksChecked
        {
            get { return _isMyBooksChecked; }
            set { _isMyBooksChecked = value; OnPropertyChanged(); }
        }

        private bool? _isFriendsChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsFriendsChecked
        {
            get { return _isFriendsChecked; }
            set { _isFriendsChecked = value; OnPropertyChanged(); }
        }

        private bool? _isProfileChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsProfileChecked
        {
            get { return _isProfileChecked; }
            set { _isProfileChecked = value; OnPropertyChanged(); }
        }

        private bool? _isSettingsChecked;
        /// <summary>
        /// 
        /// </summary>
        public bool? IsSettingsChecked
        {
            get { return _isSettingsChecked; }
            set { _isSettingsChecked = value; OnPropertyChanged(); }
        }
        #endregion

        private bool _isPaneOpen;
        /// <summary>
        /// 
        /// </summary>
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { _isPaneOpen = value; OnPropertyChanged();  }
        }

        public void MenuClick()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        public void SearchClick()
        {
            NavigationService.Navigate(typeof(Pages.SearchPage));
        }

        public void HomeClick()
        {
            NavigationService.Navigate(typeof(Pages.HomePage));
        }
        public void MyBooksClick()
        {
            NavigationService.Navigate(typeof(Pages.MyBooksPage));
        }
        public void FriendsClick()
        {
            NavigationService.Navigate(typeof(Pages.FriendsPage));
        }
        public void ProfileClick()
        {
            NavigationService.Navigate(typeof(Pages.UserPage));
        }
        public void SettingsClick()
        {
            NavigationService.Navigate(typeof(Pages.SettingsPage));
        }

        public async Task RefreshUserId()
        {
            if (!userService.IsUserIdAvailable)
                await userService.GetUserID();
        }
    }
}
