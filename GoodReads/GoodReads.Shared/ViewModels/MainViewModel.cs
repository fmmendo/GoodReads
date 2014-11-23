using GoodReads.API;
using GoodReads.API.Utilities;
using GoodReads.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace GoodReads.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Singleton
        private static MainViewModel instance = null;
        private static readonly object padlock = new object();

        public static MainViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion

        #region Bindable Properties
        /// <summary>
        /// User image (large)
        /// </summary>
        public String UserImageUrl
        {
            get
            {
                //return _gr.GoodreadsUserImageUrl;
                return UserSettings.Settings.GoodreadsUserImageUrl;
            }
        }

        /// <summary>
        /// User image (small)
        /// </summary>
        public String UserSmallImageUrl
        {
            get
            {
                //return _gr.GoodreadsUserSmallImageUrl;
                return UserSettings.Settings.GoodreadsUserSmallImageUrl ?? String.Empty;
            }
        }

        private ObservableCollection<UpdateViewModel> updates = new ObservableCollection<UpdateViewModel>();
        /// <summary>
        /// Update feed
        /// </summary>
        public ObservableCollection<UpdateViewModel> Updates
        {
            get { return updates; }
            set
            {
                updates = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ReviewViewModel> reviews = new ObservableCollection<ReviewViewModel>();
        /// <summary>
        /// User reviews (books added to shelves)
        /// </summary>
        public ObservableCollection<ReviewViewModel> Reviews
        {
            get { return reviews; }
            set
            {
                reviews = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<NotificationViewModel> notifications = new ObservableCollection<NotificationViewModel>();
        /// <summary>
        /// User notifications
        /// </summary>
        public ObservableCollection<NotificationViewModel> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility NotificationsVisibility { get { return NewNotifications > 0 ? Visibility.Visible : Visibility.Collapsed; } }
        public int NewNotifications { get { return Notifications.Count(n => n.New); } }

        private ObservableCollection<UserStatusViewModel> currentlyReading = new ObservableCollection<UserStatusViewModel>();
        /// <summary>
        /// Books in the currently-reading shelf
        /// </summary>
        public ObservableCollection<UserStatusViewModel> CurrentlyReading
        {
            get { return currentlyReading; }
        }

        /// <summary>
        /// Books from all the other shelves
        /// </summary>
        public IEnumerable<ReviewViewModel> FrontPageBooks
        {
            get { return reviews; }
        }

        public ReviewViewModel SelectedBook { get; set; }

        #endregion

        #region Commands
        /// <summary>
        /// Command called when a user clicks a book in the MY BOOKS section
        /// </summary>
        public RelayCommand<ItemClickEventArgs> BookClickCommand { get; set; }

        /// <summary>
        /// Handles the click event on a book
        /// </summary>
        /// <param name="args"></param>
        public void UserClick(ItemClickEventArgs args)
        {
            var review = args.ClickedItem as ReviewViewModel;
            if (review == null) return;

            App.NavigationService.Navigate(typeof(BookPage), review);
        }

        /// <summary>
        /// Command called when a user clicks a book in the MY BOOKS section
        /// </summary>
        public RelayCommand<TappedRoutedEventArgs> MyProfileClickCommand { get; set; }

        /// <summary>
        /// Handles the click event on a book
        /// </summary>
        /// <param name="args"></param>
        public void MyProfileTapped(TappedRoutedEventArgs args)
        {
            //App.NavigationService.Navigate(typeof(UserView), new UserViewModel(_gr.GoodreadsUserID));
            App.NavigationService.Navigate(typeof(UserView), new UserViewModel(UserSettings.Settings.GoodreadsUserID));
        }
        #endregion
        
        private GoodReadsAPI _gr = App.Goodreads;

        private MainViewModel()
        {
            PopulateData();

            BookClickCommand = new RelayCommand<ItemClickEventArgs>(UserClick);
            MyProfileClickCommand = new RelayCommand<TappedRoutedEventArgs>(MyProfileTapped);
        }

        private async void PopulateData()
        {
            //if (!_gr.IsUserAuthenticated)
            if (!UserSettings.Settings.IsUserAuthenticated)
                await _gr.Authenticate();

            NotifyPropertyChanged("UserSmallImageUrl");

            await GetNotifications();

            //these work!
            //var statusid = await _gr.PostUserStatus("", "", "", "test post from app");
            //await _gr.GetUserStatus(statusid);
            //await _gr.DeleteUserStatus(statusid);
            ////await _gr.GetReadStatus();

            await RefreshUpdates();
            await GetUserShelves();
            await GetShelfBooks();

            await GetUserProfile();
        }

        #region API calls
        private async Task GetUserProfile()
        {
            var user = await _gr.GetUserInfo();
            currentlyReading = new ObservableCollection<UserStatusViewModel>();
            foreach (var status in user.User_statuses.User_status)
            {
                var book = new UserStatusViewModel(status);
                book.PostedUpdate += book_PostedUpdate;
                CurrentlyReading.Add(book);
            }
            NotifyPropertyChanged("CurrentlyReading");
        }

        async void book_PostedUpdate(object sender, EventArgs e)
        {
            await GetUserProfile();
        }


        private async Task GetCurrentlyReadingBooks()
        {
            var result = await _gr.GetShelfBooks("currently-reading");

            foreach (var item in result.Review)
            {
                var itemVM = new ReviewViewModel(item);
                if (reviews.Any(r => r.Id == item.Id))
                    continue;

                reviews.Add(itemVM);
            }

            NotifyPropertyChanged("Reviews");
            NotifyPropertyChanged("FrontPageBooks");
            NotifyPropertyChanged("CurrentlyReading");
        }

        private async Task GetShelfBooks()
        {
            var result = await _gr.GetShelfBooks();

            foreach (var item in result.Review)
            {
                var itemVM = new ReviewViewModel(item);
                if (reviews.Any(r => r.Id == item.Id))
                    continue;

                reviews.Add(itemVM);
            }

            NotifyPropertyChanged("Reviews");
            NotifyPropertyChanged("FrontPageBooks");
            NotifyPropertyChanged("CurrentlyReading");
        }

        private async Task GetUserShelves()
        {
            _gr.GoodreadsUserShelves = await _gr.GetShelvesList();
        }

        private async Task RefreshUpdates()
        {
            var result = await _gr.GetFriendUpdates("", "", "");
            foreach (var item in result.Update)
            {
                var update = new UpdateViewModel(item);
                Updates.Add(update);
            }
            NotifyPropertyChanged("Updates");
        }

        private async Task GetNotifications()
        {
            var results = await _gr.GetNotifications();
            foreach (var notification in results.Notification)
            {
                Notifications.Add(new NotificationViewModel(notification));
            }
            NotifyPropertyChanged("Notifications");
            NotifyPropertyChanged("NewNotifications");
        }
        #endregion
    }
}
