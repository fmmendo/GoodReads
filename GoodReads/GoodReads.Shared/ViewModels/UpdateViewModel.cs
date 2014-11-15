using GoodReads.API;
using GoodReads.API.Model;
using GoodReads.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;

namespace GoodReads.ViewModels
{
    public class UpdateViewModel : ViewModelBase
    {
        private Update update;
        private GoodReadsAPI _gr = App.Goodreads;
        private String resourceType = String.Empty;
        
        private BookViewModel book;
        /// <summary>
        /// 
        /// </summary>
        public BookViewModel Book
        {
            get { return book; }
            set
            {
                book = value;
                NotifyPropertyChanged();
            }
        }

        public String ActionText
        {
            get
            {
                if (update.Action_text.Contains("<"))
                {
                    return Regex.Replace(update.Action_text, "<.*?>", string.Empty);
                }
                else
                    return update.Action_text;
            }
        }

        public String TargetImageUrl
        {
            get
            {
                if (update.Image_url != UserImageUrl)
                    return update.Image_url;
                else if (TargetIsBook)
                    return update.Object.User_status.Book.Image_url;
                else
                    return String.Empty;
            }
        }

        public String UserName { get { return update.Actor.Name; } }

        public String UserId { get { return update.Actor.Id; } }

        public String UserImageUrl { get { return update.Actor.Image_url; } }

        public String UpdatedAt
        {
            get
            {
                return String.IsNullOrEmpty(update.Updated_at)
                    ? String.Empty
                    : DateTime.Parse(update.Updated_at).ToString("f");
            }
        }

        public String ResourceId
        {
            get
            {
                if (update == null || update.Object == null)
                    return null;

                if (update.Object.Read_status != null)
                {
                    resourceType = "ReadStatus";
                    return update.Object.Read_status.Id;
                }

                if (update.Object.User_status != null)
                {
                    resourceType = "UserStatus";
                    return update.Object.User_status.Id;
                }

                return null;
            }
        }

        #region Target Is Book
        public bool TargetIsBook
        {
            get
            {
                if (book != null)
                    return true;

                if (update.Type == "review")
                {
                    return update.Object != null &&
                           update.Object.Book != null;
                }

                if (update.Type == "readstatus")
                {
                    return update.Object != null &&
                           update.Object.Read_status != null &&
                           update.Object.Read_status.Review != null &&
                           update.Object.Read_status.Review.Book != null;
                }
                return false;
            }
        }

        public String BookTitle
        {
            get
            {
                if (book != null)
                    return book.BookTitle;
                //else if (TargetIsBook)
                //{
                //    if (update.Type == "review")
                //        return update.Object.Book.Title;

                //    if (update.Type == "readstatus")
                //        return update.Object.Read_status.Review.Book.Title;
                //}

                return String.Empty;
            }
        }

        public String BookAuthor
        {
            get
            {
                if (book != null)
                    return book.BookAuthorName;
                //else if (TargetIsBook)
                //{
                //    if (update.Type == "review")
                //        return update.Object.Book.Authors.Author.Name;

                //    if (update.Type == "readstatus")
                //        return update.Object.Read_status.Review.Book.Author.Name;
                //}

                return String.Empty;
            }
        }

        public String BookCoverImageURL
        {
            get
            {
                if (book != null)
                    return book.BookImageURL;
                //else if (TargetIsBook)
                //{
                //    if (update.Type == "review")
                //        return update.Object.Book.Image_url;

                //    if (update.Type == "readstatus")
                //        return update.Object.Read_status.Review.Book.Image_url;
                //}
                return String.Empty;
            }
        }

        public String BookId
        {
            get
            {
                if (book != null)
                {
                    return book.Id;
                    //if (update.Type == "review")
                    //    return update.Object.Book.Id;

                    //if (update.Type == "readstatus")
                    //    return update.Object.Read_status.Review.Book.Id;
                }
                return String.Empty;
            }
        }
        #endregion

        //public IEnumerable<UserShelf> Shelves { get { return _gr.GoodreadsUserShelves; } }

        #region UI Properties
        /// <summary>
        /// 
        /// </summary>
        public Visibility CanCommentOrLike
        {
            get { return String.IsNullOrEmpty(ResourceId) ? Visibility.Collapsed : Visibility.Visible; }
        }

        private Visibility isWritingComment = Visibility.Collapsed;
        /// <summary>
        /// 
        /// </summary>
        public Visibility IsWritingComment
        {
            get { return isWritingComment; }
            set
            {
                isWritingComment = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility detailsVisibility = Visibility.Collapsed;
        /// <summary>
        /// 
        /// </summary>
        public Visibility DetailsVisibility
        {
            get { return detailsVisibility; }
            set
            {
                detailsVisibility = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("IsCollapsed");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Visibility IsCollapsed
        {
            get { return detailsVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; }

        }

        private String commentButtonText = "Cancel";
        /// <summary>
        /// 
        /// </summary>
        public String CommentButtonText
        {
            get { return commentButtonText; }
            set
            {
                commentButtonText = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public UpdateViewModel(Update update)
        {
            this.update = update;

            if (TargetIsBook)
            {
                if (update.Type == "review" && update.Object != null && update.Object.Book != null)
                    book = new BookViewModel(update.Object.Book);

                if (update.Type == "readstatus" && update.Object != null &&
                           update.Object.Read_status != null &&
                           update.Object.Read_status.Review != null &&
                           update.Object.Read_status.Review.Book != null)
                    book = new BookViewModel(update.Object.Read_status.Review.Book);

                NotifyPropertyChanged("Book");
            }

            UserClickCommand = new RelayCommand(UserClick);
            WriteCommentCommand = new RelayCommand(WriteCommentClick);
            PostCommentCommand = new RelayCommand(PostCommentClick);
            ToggleDetailsCommand = new RelayCommand(ToggleDetailsClick);
            LikeCommand = new RelayCommand(LikeClick);
            //WantToReadCommand = new RelayCommand(WantToReadClick);
            AddToShelfCommand = new RelayCommand<object>(AddToShelfClick);
        }

        #region Commands
        public RelayCommand UserClickCommand { get; set; }
        public RelayCommand WriteCommentCommand { get; set; }
        public RelayCommand PostCommentCommand { get; set; }
        public RelayCommand ToggleDetailsCommand { get; set; }
        public RelayCommand LikeCommand { get; set; }
        //public RelayCommand WantToReadCommand { get; set; }
        public RelayCommand<object> AddToShelfCommand { get; set; }

        public void UserClick()
        {
            App.NavigationService.Navigate(typeof(UserView), new UserViewModel(UserId));
        }

        public async void LikeClick()
        {
            if (String.IsNullOrEmpty(ResourceId) || String.IsNullOrEmpty(resourceType))
                return;

            var result = await _gr.LikeResource(ResourceId, resourceType);
            // var result = await _gr.AddComment(ResourceId, update.Type, "test");
        }

        //public async void WantToReadClick()
        //{
        //    if (String.IsNullOrEmpty(BookId) || String.IsNullOrEmpty(resourceType))
        //        return;

        //    var result = await _gr.AddBookToShelf("to-read", BookId);
        //    // var result = await _gr.AddComment(ResourceId, update.4Type, "test");
        //}

        public async void AddToShelfClick(object param)
        {
            if (String.IsNullOrEmpty(BookId) || String.IsNullOrEmpty(resourceType))
                return;

            var result = await _gr.AddBookToShelf("to-read", BookId);
            // var result = await _gr.AddComment(ResourceId, update.Type, "test");
        }

        public void PostCommentClick()
        {
            if (CommentButtonText.Equals("Cancel"))
                IsWritingComment = Visibility.Collapsed;
            //else, post comment
        }

        public void WriteCommentClick()
        {
            IsWritingComment = Visibility.Visible;
        }


        #endregion

        public async void ToggleDetailsClick()
        {
            if (!TargetIsBook) return;

            DetailsVisibility = detailsVisibility == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;


            if (update.Type == "review" && update.Object != null && update.Object.Book != null)
                book = new BookViewModel(await _gr.GetBookInfo(update.Object.Book.Id));

            if (update.Type == "readstatus" && update.Object != null &&
                       update.Object.Read_status != null &&
                       update.Object.Read_status.Review != null &&
                       update.Object.Read_status.Review.Book != null)
                book = new BookViewModel(await _gr.GetBookInfo(update.Object.Read_status.Review.Book.Id));

            NotifyPropertyChanged("Book");

            //book = new BookViewModel( await _gr.GetBookInfo(BookId));
            //NotifyPropertyChanged("Book");
            NotifyPropertyChanged("BookTitle");
            NotifyPropertyChanged("BookAuthor");
            NotifyPropertyChanged("BookCoverImageURL");
            NotifyPropertyChanged("Shelves");
        }
    }
}
