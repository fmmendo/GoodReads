using GoodReads.API;
using GoodReads.API.Model;
using GoodReads.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.Data.Html;
using System.Collections.ObjectModel;

namespace GoodReads.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        private GoodReadsAPI _gr = App.Goodreads;
        private Author author;

        #region Bindable Properties
        public String ImageUrl { get { return author != null ? author.ImageUrl : String.Empty; } }
        public String Name { get { return author != null ? author.Name : String.Empty; } }
        public String Url { get { return author != null ? author.Url : String.Empty; } }
        public String Hometown { get { return author != null ? author.Hometown : String.Empty; } }
        public String BornAt { get { return author != null ? author.BornAt : String.Empty; } }
        public String DiedAt { get { return author != null ? author.DiedAt : String.Empty; } }
        public String Genre { get { return author != null ? author.Genre1 : String.Empty; } }
        public String Gender { get { return author != null ? author.Gender : String.Empty; } }
        public String Link { get { return author != null ? author.Link : String.Empty; } }
        public String CreatedAt { get { return author != null ? author.CreatedAt : String.Empty; } }
        public String About { get { return author != null && !String.IsNullOrEmpty(author.About) ? HtmlUtilities.ConvertToText(author.About) : String.Empty; } }
        public String Rating { get { return author != null ? author.AverageRating : String.Empty; } }
        public String Influences { get { return author != null ? author.Influences : String.Empty; } }
        public String WorksCount { get { return author != null ? author.WorksCount : String.Empty; } }

        private ObservableCollection<BookViewModel> books = new ObservableCollection<BookViewModel>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<BookViewModel> Books
        {
            get { return books; }
            set
            {
                books = value;
                NotifyPropertyChanged();
            }
        }

        public BookViewModel SelectedBook { get; set; }
        #endregion

        public AuthorViewModel(Author author)
        {
            this.author = author;

            BookClickCommand = new RelayCommand<ItemClickEventArgs>(UserClick);
        }

        public AuthorViewModel(string id)
        {
            LoadFullData(id);

            BookClickCommand = new RelayCommand<ItemClickEventArgs>(UserClick);
        }

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
            var book = args.ClickedItem as BookViewModel;
            if (book == null) return;

            App.NavigationService.Navigate(typeof(BookPage), book);
        }
        #endregion

        public async Task LoadFullData(string id = null)
        {
            author = id == null
                ? await _gr.GetAuthorInfo(author.Id)
                : await _gr.GetAuthorInfo(id);

            NotifyPropertyChanged("ImageUrl");
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Url");
            NotifyPropertyChanged("Hometown");
            NotifyPropertyChanged("BornAt");
            NotifyPropertyChanged("DiedAt");
            NotifyPropertyChanged("Genre");
            NotifyPropertyChanged("Gender");
            NotifyPropertyChanged("Link");
            NotifyPropertyChanged("CreatedAt");
            NotifyPropertyChanged("About");
            NotifyPropertyChanged("Rating");
            NotifyPropertyChanged("Influences");
            NotifyPropertyChanged("WorksCount");

            var authorBooks = await _gr.GetAuthorBooks(author.Id);
            foreach (var book in authorBooks.Book)
            {
                Books.Add(new BookViewModel(book));
            }
            NotifyPropertyChanged("Books");
        }
    }
}
