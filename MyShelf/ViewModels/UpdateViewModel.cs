using Mendo.UAP.Common;
using MyShelf.API.Services;
using MyShelf.API.XML;
using MyShelf.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class UpdateViewModel : ViewModelBase
    {
        private string _resourceId;
        private string _resourceType;

        public string ActionText { get; set; }
        public string UserName { get; set; }
        //public string Link { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBook { get; set; } = false;
        //public User Actor { get; set; }
        public string UpdatedAt { get; set; }
        //public UpdateObject Object { get; set; }
        //public string Type { get; set; }
        //public UpdateAction Action { get; set; }
        //public string Body { get; set; }

        public Uri BookImageUrl { get; set; }
        public string BookAuthor { get; set; }
        public string BookTitle { get; set; }

        private string BookId;
        private string UserId;
        private string AuthorId;

        public UpdateViewModel(Update update)
        {
            if (update == null)
                return;

            ImageUrl = update.Actor?.ImageUrl;
            UserName = update.Actor?.Name;
            ActionText = update.ActionText.Contains("<") ? Regex.Replace(update.ActionText, "<.*?>", string.Empty) : update.ActionText;

            DateTime date;
            if (DateTime.TryParse(update.UpdatedAt, out date))
                UpdatedAt = date.ToString("dd MMM yyyy");
                //UpdatedAt = date.ToString("ddd, dd MMM yyyy HH:mm:ss");

            UserId = update.Actor.Id;

            if (update.Type.Equals("review") && update.Object?.Book != null)
                SetUpBookData(update.Object.Book);
            else if (update.Type.Equals("readstatus") && update.Object?.ReadStatus?.Review?.Book != null)
                SetUpBookData(update.Object.ReadStatus.Review.Book);
            else
                IsBook = false;

            if (update.Object?.ReadStatus != null)
            {
                _resourceId = update.Object.ReadStatus.Id;
                _resourceType = "ReadStatus";
            }
            if (update.Object?.UserStatus != null)
            {
                _resourceId = update.Object.UserStatus.Id;
                _resourceType = "UserStatus";
            }

        }

        private void SetUpBookData(Book book)
        {
            IsBook = true;

            //BookId = book.Id;
            //AuthorId = book.AuthorId;
            //WorkId = book.Work_id;

            GetBookInfoAsync(book);
        }

        private async Task GetBookInfoAsync(Book book)
        {
            var result = await BookService.Instance.GetBookInfo(book.Id);

            BookImageUrl = new Uri(result.ImageUrl);
            BookAuthor = result.Authors.FirstOrDefault().Name;//string.Join(", ", result.Authors.Select(a => a.Name));
            BookTitle = result.Title;

            BookId = result.Id;
            AuthorId = result.Authors.FirstOrDefault().Id;

            OnPropertyChanged("BookImageUrl");
            OnPropertyChanged("BookAuthor");
            OnPropertyChanged("BookTitle");
        }

        public void UserClick()
        {
            NavigationService.Navigate(typeof(UserPage), UserId);
        }

        public void BookClick()
        {
            NavigationService.Navigate(typeof(BookPage), BookId);
        }

        public void AuthorClick()
        {
            NavigationService.Navigate(typeof(AuthorPage), AuthorId);
        }

        public void LikeClick()
        {
            UserService.Instance.LikeResource(_resourceId, _resourceType);
        }

        public void CommentClick()
        {
        }
    }
}
