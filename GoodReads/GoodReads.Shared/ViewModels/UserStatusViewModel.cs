using GoodReads.API;
using GoodReads.API.Model;
using GoodReads.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace GoodReads.ViewModels
{
    public class UserStatusViewModel : ViewModelBase
    {
        UserStatus status;
        GoodReadsAPI _gr = App.Goodreads;

        public event EventHandler PostedUpdate;

        #region BindableProperties
        public String Body { get { return status.Body; } }

        public String Book_id { get { return status.Book_id; } }

        public String Chapter { get { return status.Chapter; } }

        public String Comments_count { get { return status.Comments_count; } }

        public String Created_at { get { return status.Created_at; } }

        public String Id { get { return status.Id; } }

        public String Last_comment_at { get { return status.Last_comment_at; } }

        public String Note_updated_at { get { return status.Note_updated_at; } }

        public String Note_uri { get { return status.Note_uri; } }

        public String Page { get { return status.Page; } }

        public String Percent { get { return status.Percent; } }

        public String Ratings_count { get { return status.Ratings_count; } }

        public String Updated_at { get { return status.Updated_at; } }

        public String User_id { get { return status.User_id; } }

        public String Work_id { get { return status.Work_id; } }

        public String Review_id { get { return status.Review_id; } }

        public Book Book { get { return status.Book; } }

        public String BookImageURL { get { return Book.Image_url; } }

        public String BookTitle { get { return Book.Title; } }

        public String BookAuthorName { get { return Book.Authors.Author.Name; } }

        public String BookPages { get { return Book.Num_pages; } }


        //public String Progress { get { return String.IsNullOrEmpty(Page) ? Percent : (100 * Int32.Parse(Page) / Int32.Parse(Book.Num_pages)).ToString(); } }
        public String Progress
        {
            get
            {
                return String.IsNullOrEmpty(Percent)
                    ? (100 * Int32.Parse(Page) / Int32.Parse(Book.Num_pages)).ToString()
                    : Percent;
            }
        }

        public String ProgressMessage
        {
            get
            {
                return String.IsNullOrEmpty(Percent)
                    ? String.Format("On page {0} of {1}", Page, Book.Num_pages)
                    : String.Format("{0}% Done", Percent);
            }
        }

        private String updateBody = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        public String UpdateBody
        {
            get { return updateBody; }
            set { updateBody = value; NotifyPropertyChanged(); }
        }

        private String updatePage = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        public String UpdatePage
        {
            get { return updatePage; }
            set { updatePage = value; NotifyPropertyChanged(); }
        }

        private String updatePercent = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        public String UpdatePercent
        {
            get { return updatePercent; }
            set { updatePercent = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region UI
        private bool isEditing = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility pageVisibility = Visibility.Collapsed;
        /// <summary>
        /// 
        /// </summary>
        public Visibility PageVisibility
        {
            get { return pageVisibility; }
            set { pageVisibility = value; NotifyPropertyChanged(); }
        }

        private Visibility percentVisibility = Visibility.Visible;
        /// <summary>
        /// 
        /// </summary>
        public Visibility PercentVisibility
        {
            get { return percentVisibility; }
            set { percentVisibility = value; NotifyPropertyChanged(); }
        }

        private Visibility editingVisibility = Visibility.Collapsed;
        /// <summary>
        /// 
        /// </summary>
        public Visibility EditingVisibility
        {
            get { return editingVisibility; }
            set
            {
                editingVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility ProgressBarVisibility { get { return String.IsNullOrEmpty(Progress) ? Visibility.Collapsed : Visibility.Visible; } }

        #endregion

        public UserStatusViewModel(UserStatus status)
        {
            this.status = status;

            PostUpdateCommand = new RelayCommand(PostUpdateClick);
            EditCommand = new RelayCommand(EditClick);
            EditModeCommand = new RelayCommand(EditModeClick);

            BookClickCommand = new RelayCommand(BookClick);
            AuthorClickCommand = new RelayCommand(AuthorClick);

        }

        public RelayCommand EditCommand { get; set; }
        public async void EditClick()
        {
            IsEditing = !isEditing;
            EditingVisibility = isEditing
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public RelayCommand EditModeCommand { get; set; }
        public async void EditModeClick()
        {
            PageVisibility = PageVisibility == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;

            PercentVisibility = PercentVisibility == Visibility.Collapsed
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public RelayCommand PostUpdateCommand { get; set; }
        //public async bool CanPostUpdate
        //{ }
        public async void PostUpdateClick()
        {
            _gr.PostStatusUpdate(Book.Id, UpdatePage, UpdatePercent, UpdateBody);

            if (PostedUpdate != null)
            {
                PostedUpdate(this, new EventArgs());
            }

        }

        public RelayCommand BookClickCommand { get; set; }
        //public async bool CanPostUpdate
        //{ }
        public async void BookClick()
        {
            App.NavigationService.Navigate(typeof(BookPage), Book.Id);
        }

        public RelayCommand AuthorClickCommand { get; set; }
        //public async bool CanPostUpdate
        //{ }
        public async void AuthorClick()
        {
            App.NavigationService.Navigate(typeof(AuthorPage), Book.Authors.Author.Id);
        }
    }
}
