using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;
using Mendo.UAP.Common;
using System.Collections.ObjectModel;
using MyShelf.API.Services;

namespace MyShelf.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Age { get; set; }
        public string About { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Joined { get; set; }
        public string LastActive { get; set; }
        public string Interests { get; set; }
        public string FavouriteBooks { get; set; }
        public string GroupsCount { get; set; }
        public string FriendsCount { get; set; }
        public string BooksCount { get; set; }

        public ObservableCollection<UserStatusViewModel> CurrentlyReading { get; set; } = new ObservableCollection<UserStatusViewModel>();
        public ObservableCollection<UpdateViewModel> Updates { get; set; } = new ObservableCollection<UpdateViewModel>();
        public ObservableCollection<ShelfViewModel> Shelves { get; set; } = new ObservableCollection<ShelfViewModel>();

        public UserViewModel(User friend)
        {
            PopulateData(friend);
        }

        public UserViewModel(string id)
        {
            GetUserInfoAsync(id);
        }

        private async Task GetUserInfoAsync(string id)
        {
            var user = await UserService.Instance.GetUserInfo(id);

            PopulateData(user);
        }

        private void PopulateData(User friend)
        {
            Name = friend.Name;
            ImageUrl = friend.ImageUrl;
            Link = friend.Link;
            Age = friend.Age;
            About = friend.About;
            Gender = friend.Gender;
            Location = friend.Location;
            Website = friend.Website;
            Joined = friend.Joined;
            LastActive = friend.LastActive;
            Interests = friend.Interests;
            FavouriteBooks = friend.FavoriteBooks;
            FriendsCount = $"{friend.FriendsCount} Friends";
            BooksCount = $"{friend.ReviewsCount} Books";
            GroupsCount = $"{friend.GroupsCount} Books";
            CurrentlyReading.Clear();

            foreach (var userStatus in friend.UserStatuses)
            {
                CurrentlyReading.Add(new UserStatusViewModel(userStatus));
            }

            Updates.Clear();
            foreach (var userUpdate in friend.Updates.Update)
            {
                Updates.Add(new UpdateViewModel(userUpdate));
            }

            Shelves.Clear();
            foreach (var shelf in friend.UserShelves)
            {
                Shelves.Add(new ShelfViewModel(shelf));
            }
        }

        public void UserClick()
        {
        }

        public void BooksClick()
        {
        }
        
        public void FriendsClick()
        {
        }
    }
}
