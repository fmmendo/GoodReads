using GoodReads.API;
using GoodReads.API.Model;
using GoodReads.Common;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GoodReads.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private GoodReadsAPI _gr = App.Goodreads;
        private User user;

        #region Bindable Properties
        public String UserName
        {
            get
            {
                return user == null ? String.Empty : user.Name;
            }
        }
        public String UserSmallImageUrl
        {
            get
            {
                return user == null ? String.Empty : user.Small_image_url;
            }
        }
        public String UserImageUrl
        {
            get
            {
                return user == null ? String.Empty : user.Image_url.Replace("p3/", "p8/");
            }
        }
        public String UserLink
        {
            get
            {
                return user == null ? String.Empty : user.Link;
            }
        }
        public String UserAge
        {
            get
            {
                return user == null ? String.Empty : user.Age;
            }
        }
        public String UserAbout
        {
            get
            {
                return user == null ? String.Empty : user.About;
            }
        }
        public String UserGender
        {
            get
            {
                return user == null ? String.Empty : user.Gender;
            }
        }
        public String UserLocation
        {
            get
            {
                return user == null ? String.Empty : user.Location;
            }
        }
        public String UserWebsite
        {
            get
            {
                return user == null ? String.Empty : user.Website;
            }
        }
        public String UserJoined
        {
            get
            {
                if (user == null)
                    return String.Empty;

                return DateTime.Parse(user.Joined).ToString("y");
            }
        }
        public String UserLastActive
        {
            get
            {
                if (user == null)
                    return String.Empty;

                return DateTime.Parse(user.Last_active).ToString("y");
            }
        }
        public String UserInterests
        {
            get
            {
                return user == null ? String.Empty : user.Interests;
            }
        }
        public String UserFavoriteBooks
        {
            get
            {
                return user == null ? String.Empty : user.Favorite_books;
            }
        }
        public String UserFriendsCount
        {
            get
            {
                return user == null ? String.Empty : user.Friends_count;
            }
        }
        public String UserGroupsCount
        {
            get
            {
                return user == null ? String.Empty : user.Groups_count;
            }
        }
        public String UserReviewsCount
        {
            get
            {
                return user == null ? String.Empty : user.Reviews_count;
            }
        }

        //public String UserFavoriteAuthors { get { return user == null ? String.Empty : user.Favorite_authors;} }

        private ObservableCollection<UserStatusViewModel> currentlyReading = new ObservableCollection<UserStatusViewModel>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<UserStatusViewModel> CurrentlyReading
        {
            get { return currentlyReading; }
            set
            {
                currentlyReading = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<UpdateViewModel> updates = new ObservableCollection<UpdateViewModel>();
        /// <summary>
        /// 
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

        private ObservableCollection<ShelvesViewModel> shelves = new ObservableCollection<ShelvesViewModel>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ShelvesViewModel> Shelves
        {
            get { return shelves; }
            set
            {
                shelves = value;
                NotifyPropertyChanged();
            }
        }

        //private ObservableCollection<UserDetail> userDetails = new ObservableCollection<UserDetail>();
        //public ObservableCollection<UserDetail> UserDetails
        //{
        //    get { return userDetails; }
        //    set
        //    {
        //        userDetails = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        #endregion

        public UserViewModel()
        {

        }

        public UserViewModel(User user)
        {
            this.user = user;
        }

        public UserViewModel(string id)
        {
            LoadFullData(id);
        }

        public async Task LoadFullData(string id = null)
        {
            user = id == null
                ? await _gr.GetUserInfo(user.Id)
                : await _gr.GetUserInfo(id);

            NotifyPropertyChanged("UserName");
            NotifyPropertyChanged("UserSmallImageUrl");
            NotifyPropertyChanged("UserImageUrl");
            NotifyPropertyChanged("UserLink");
            NotifyPropertyChanged("UserAge");
            NotifyPropertyChanged("UserAbout");
            NotifyPropertyChanged("UserGender");
            NotifyPropertyChanged("UserLocation");
            NotifyPropertyChanged("UserWebsite");
            NotifyPropertyChanged("UserJoined");
            NotifyPropertyChanged("UserLastActive");
            NotifyPropertyChanged("UserInterests");
            NotifyPropertyChanged("UserFavoriteBooks");
            NotifyPropertyChanged("UserFriendsCount");
            NotifyPropertyChanged("UserGroupsCount");
            NotifyPropertyChanged("UserReviewsCount");

            //GenerateUserDetails();

            CurrentlyReading.Clear();
            foreach (var userStatus in user.User_statuses.User_status)
            {
                CurrentlyReading.Add(new UserStatusViewModel(userStatus));
            }

            Updates.Clear();
            foreach (var userUpdate in user.Updates.Update)
            {
                Updates.Add(new UpdateViewModel(userUpdate));
            }

            Shelves.Clear();
            foreach (var shelf in user.User_shelves.User_shelf)
            {
                Shelves.Add(new ShelvesViewModel(shelf));
            }
        }

        //private void GenerateUserDetails()
        //{
        //    var sb = new StringBuilder();
        //    if (!String.IsNullOrEmpty(UserAge))
        //        sb.Append(String.Format("Age {0}", UserAge));
        //    if (!String.IsNullOrEmpty(UserAge) && !String.IsNullOrEmpty(UserGender))
        //        sb.Append(", ");
        //    if (!String.IsNullOrEmpty(UserGender))
        //        sb.Append(String.Format("{0}", UserGender));

        //    if (sb.Length > 0)
        //        UserDetails.Add(new UserDetail { Key = "details", Value = sb.ToString() });

        //    if (!String.IsNullOrEmpty(UserLocation))
        //        UserDetails.Add(new UserDetail { Key = "location", Value = UserLocation });

        //    if (!String.IsNullOrEmpty(UserWebsite))
        //        UserDetails.Add(new UserDetail { Key = "webite", Value = UserWebsite });

        //    var joined = DateTime.Parse(UserJoined);
        //    var active = DateTime.Parse(UserLastActive);
        //    UserDetails.Add(new UserDetail { Key = "activity", Value = String.Format("Joined {0}, last active {1}", joined.ToString("MMM yyyy"), active.ToString("MMM yyyy")) });

        //    if (!String.IsNullOrEmpty(UserInterests))
        //        UserDetails.Add(new UserDetail { Key = "interests", Value = UserInterests });
        //    if (!String.IsNullOrEmpty(UserFavoriteBooks))
        //        UserDetails.Add(new UserDetail { Key = "favorite books", Value = UserFavoriteBooks });
        //    if (!String.IsNullOrEmpty(UserAbout))
        //        UserDetails.Add(new UserDetail { Key = "about", Value = UserAbout });

        //    NotifyPropertyChanged("UserDetails");

        //}
    }

    public class UserDetail
    {
        public String Key { get; set; }
        public String Value { get; set; }
    }
}
