﻿using Mendo.UAP.Common;
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
        public string ActionText { get; set; }
        public string UserName { get; set; }
        //public string Link { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBook { get; set; } = false;
        //public User Actor { get; set; }
        //public string UpdatedAt { get; set; }
        //public UpdateObject Object { get; set; }
        //public string Type { get; set; }
        //public UpdateAction Action { get; set; }
        //public string Body { get; set; }

        public string BookImageUrl { get; set; }
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

            UserId = update.Actor.Id;

            if (update.Type.Equals("review") && update.Object?.Book != null)
                SetUpBookData(update.Object.Book);
            else if (update.Type.Equals("readstatus") && update.Object?.Read_status?.Review?.Book != null)
                SetUpBookData(update.Object.Read_status.Review.Book);
            else
                IsBook = false;
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

            BookImageUrl = result.ImageUrl;
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
    }
}
