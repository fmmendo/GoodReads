using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;
using MyShelf.Pages;
using MyShelf.API.Services;

namespace MyShelf.ViewModels
{
    public class UserStatusViewModel : ViewModelBase
    {
        public string BookTitle { get; set; }
        public string BookImageUrl { get; set; }
        public string BookAuthor { get; set; }
        public double Percent { get; set; }
        public string Page { get; set; }
        public bool Updating { get { return Get(false); } set { Set(value); } }
        public bool UpdatingPage { get { return Get(true); } set { Set(value); } }
        public string BookPages { get; set; }
        public string BookId { get; set; }

        public string UpdatePercentage { get { return Get(Percent.ToString()); } set { Set(value); } }
        public string UpdatePageNum { get { return Get(Page); } set { Set(value); } }
        public string UpdateText { get { return Get(string.Empty); } set { Set(value); } }

        private string authorId;

        public UserStatusViewModel(UserStatus status)
        {
            BookTitle = status.Book.Title;
            BookImageUrl = status.Book.ImageUrl;
            BookAuthor = string.Join(", ", status.Book.Authors.Select(a => a.Name));
            BookPages = status.Book.NumPages;
            Percent = double.Parse(status.Percent);
            Page = status.Page;
            BookId = status.Book.Id;
            authorId = status.Book.Authors.FirstOrDefault()?.Id;

            Updating = false;
        }

        public UserStatusViewModel(Update update)
        {
            BookTitle = update?.Object?.ReadStatus?.Review?.Book?.Title;
            BookImageUrl = update?.Object?.ReadStatus?.Review?.Book?.ImageUrl;
            BookAuthor = string.Join(", ", update?.Object?.ReadStatus?.Review?.Book?.Authors.Select(a => a.Name));
            BookPages = update?.Object?.ReadStatus?.Review?.Book?.NumPages;
            Percent = 0d;
            Page = "0";
            BookId = update?.Object?.ReadStatus?.Review?.Book?.Id;
            authorId = update?.Object?.ReadStatus?.Review?.Book?.Authors.FirstOrDefault()?.Id;

            Updating = false;
        }

        public void BookClick()
        {
            NavigationService.Navigate(typeof(BookPage), BookId);
        }

        public void AuthorClick()
        {
            NavigationService.Navigate(typeof(AuthorPage), authorId);
        }

        public void ToggleUpdating()
        {
            Updating = !Updating;
        }

        public void ToggleUpdateMode()
        {
            UpdatingPage = !UpdatingPage;
        }

        public async void PostUpdate()
        {
            var result = await UserService.Instance.PostStatusUpdate(BookId, 
                                                                     UpdatingPage ? UpdatePageNum : null, 
                                                                     UpdatingPage ? null : UpdatePercentage, 
                                                                     UpdateText);
            if (!String.IsNullOrEmpty(result))
                ToggleUpdating();
        }
    }
}
