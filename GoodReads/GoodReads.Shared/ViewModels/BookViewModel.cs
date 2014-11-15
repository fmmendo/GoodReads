using GoodReads.API;
using GoodReads.API.Model;
using GoodReads.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Html;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GoodReads.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private GoodReadsAPI _gr = App.Goodreads;
        private Book book;

        #region Bindable Properties
        public Work Work { get { return book == null ? null : book.Work; } }

        public String Asin { get { return book == null ? String.Empty : book.Asin; } }

        public String BookAuthorName
        {
            get
            {
                if (book != null && book.Author != null && book.Author.Name != null)
                    return book.Author.Name;
                if (book != null && book.Authors != null && book.Authors.Author != null)
                    return book.Authors.Author.Name;

                return String.Empty;
            }
        }

        public String Description { get { return book == null ? String.Empty : HtmlUtilities.ConvertToText(book.Description); } }

        public String Edition { get { return book == null ? String.Empty : book.Edition_information; } }

        public String Format { get { return book == null ? String.Empty : book.Format; } }

        public String Id { get { return book == null ? String.Empty : book.Id; } }

        public String BookImageURL
        {
            get
            {
                if (book == null || String.IsNullOrEmpty(book.Image_url))
                    return String.Empty;

                if (book.Image_url.Contains("nophoto"))
                    return book.Image_url;

                string url = book.Image_url;
                var m = url.LastIndexOf("m/");
                if (m > 0)
                {
                    url = url.Remove(m, 1);
                    url = url.Insert(m, "l");
                }
                else
                {
                    var s = url.LastIndexOf("s/");
                    if (s > 0)
                    {
                        url.Remove(s, 1);
                        url.Insert(s, "l");
                    }
                }
                return url;
            }
        }

        public String Isbn { get { return book == null ? String.Empty : book.Isbn; } }

        public String Isbn13 { get { return book == null ? String.Empty : book.Isbn13; } }

        public bool IsEbook { get { return book == null || String.IsNullOrEmpty(book.IsEbook) ? false : bool.Parse(book.IsEbook); } }

        public String Language { get { return book == null ? String.Empty : book.Language_code; } }

        public String LargeImageURL { get { return String.IsNullOrEmpty(book.Large_image_url) ? BookImageURL : book.Large_image_url; } }

        public String Link { get { return book == null ? String.Empty : book.Link; } }

        public int NumberOfPages { get { return String.IsNullOrEmpty(book.Num_pages) ? 0 : Int32.Parse(book.Num_pages); } }

        public String OriginalTitle { get { return book != null && book.Work != null ? book.Work.Original_title : String.Empty; } }

        public String OriginalPublishDateString { get { return OriginalPublishDate.ToString("dd MMM yyyy"); } }
        public DateTime OriginalPublishDate
        {
            get
            {
                return Work == null ? new DateTime()
                                    : new DateTime(Int32.Parse(Work.Original_publication_year),
                                                   Int32.Parse(Work.Original_publication_month),
                                                   Int32.Parse(Work.Original_publication_day));
            }
        }

        public String PublishDateString { get { return PublishDate.ToString("dd MMM yyyy"); } }
        public DateTime PublishDate
        {
            get
            {
                return Work == null ? new DateTime()
                                    : new DateTime(Int32.Parse(book.Publication_year),
                                                   Int32.Parse(book.Publication_month),
                                                   Int32.Parse(book.Publication_day));
            }
        }

        public String Publisher { get { return book.Publisher; } }

        public double Rating { get { return book == null || String.IsNullOrEmpty(book.Average_rating) ? 0.0 : Double.Parse(book.Average_rating); } }

        public double MyRating 
        {
            get
            {
                if (_gr.GoodreadsReviews == null || _gr.GoodreadsReviews.Review == null || _gr.GoodreadsReviews.Review.Count <= 0)
                    return 0.0;
                var review = _gr.GoodreadsReviews.Review.FirstOrDefault(r => r.Book.Id.Equals(Id));
                if (review == null)
                    return 0.0;
                return Double.Parse(review.Rating);
            }
        }

        public bool NotInShelf
        {
            get
            {
                if (_gr.GoodreadsReviews == null || _gr.GoodreadsReviews.Review == null || _gr.GoodreadsReviews.Review.Count <= 0)
                    return true;
                var review = _gr.GoodreadsReviews.Review.FirstOrDefault(r => r.Book.Id.Equals(Id));
                if (review == null)
                    return true;

                return !review.Shelves.Shelf.Name.Equals("to-read");
            }
        }

        public int RatingsCount
        {
            get
            {
                if (Work == null)
                    return 0;
                else
                    return String.IsNullOrEmpty(Work.Ratings_count) ? 0 : Int32.Parse(Work.Ratings_count);
            }
        }

        public int TextReviewsCount
        {
            get
            {
                if (Work == null)
                    return 0;
                else
                    return String.IsNullOrEmpty(Work.Text_reviews_count) ? 0 : Int32.Parse(Work.Text_reviews_count);
            }
        }

        public String BookTitle { get { return book == null ? String.Empty : book.Title; } }

        public String Url { get { return book.Url; } }

        public IEnumerable<UserShelf> Shelves { get { return _gr.GoodreadsUserShelves; } }

        private UserShelf selectedShelf;

        public UserShelf SelectedShelf
        {
            get { return selectedShelf; }
            set
            {
                if (value != null)
                {
                    selectedShelf = value;
                    _gr.AddBookToShelf(value.Name, Id);
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<BookDetail> details = new ObservableCollection<BookDetail>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<BookDetail> Details
        {
            get { return details; }
            set
            {
                details = value;
                NotifyPropertyChanged();
            }
        }

        private string stats = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Stats
        {
            get { return stats; }
            set
            {
                stats = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ReviewViewModel> reviews = new ObservableCollection<ReviewViewModel>();

        public ObservableCollection<ReviewViewModel> Reviews
        {
            get { return reviews; }
            set
            {
                reviews = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public RelayCommand<RoutedEventArgs> AuthorClickCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public async void AuthorClick(RoutedEventArgs args)
        {
            var author = new AuthorViewModel(book.Authors.Author);

            App.NavigationService.Navigate(typeof(AuthorPage), author);
        }

        public RelayCommand WantToReadCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public async void WantToReadClick()
        {
            if (String.IsNullOrEmpty(Id)/* || String.IsNullOrEmpty(resourceType)*/)
                return;

            var result = await _gr.AddBookToShelf("to-read", Id);
        }

        public RelayCommand BookClickCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public async void BookClick()
        {
            App.NavigationService.Navigate(typeof(BookPage), Id);
        }
        #endregion

        public BookViewModel(Book book)
        {
            this.book = book;

            AuthorClickCommand = new RelayCommand<RoutedEventArgs>(AuthorClick);
            WantToReadCommand = new RelayCommand(WantToReadClick);
            BookClickCommand = new RelayCommand(BookClick);
        }

        public BookViewModel(string id)
        {
            LoadFullData(id);

            AuthorClickCommand = new RelayCommand<RoutedEventArgs>(AuthorClick);
            WantToReadCommand = new RelayCommand(WantToReadClick);
            BookClickCommand = new RelayCommand(BookClick);
        }

        public async Task LoadFullData(string bookid = null)
        {
            book = bookid == null ? await _gr.GetBookInfo(Id) : await _gr.GetBookInfo(bookid);

            NotifyPropertyChanged("BookImageURL");
            NotifyPropertyChanged("OriginalTitle");
            NotifyPropertyChanged("BookTitle");
            NotifyPropertyChanged("RatingsCount");
            NotifyPropertyChanged("TextReviewsCount");
            NotifyPropertyChanged("Publisher");
            NotifyPropertyChanged("OriginalPublishDate");
            NotifyPropertyChanged("OriginalPublishDateString");
            NotifyPropertyChanged("PublishDateString");
            NotifyPropertyChanged("PublishDate");
            NotifyPropertyChanged("NumberOfPages");
            NotifyPropertyChanged("Url");
            NotifyPropertyChanged("Language");
            NotifyPropertyChanged("Rating");
            NotifyPropertyChanged("Description");
            NotifyPropertyChanged("NotInShelf");

            BuildBookDetailsList();
            NotifyPropertyChanged("Details");

            Reviews = new ObservableCollection<ReviewViewModel>();
            foreach (var review in book.Reviews.Review)
            {
                Reviews.Add(new ReviewViewModel(review));
            }
            NotifyPropertyChanged("Reviews");
        }

        private void BuildBookDetailsList()
        {
            //format
            string format = String.Empty;
            if (!String.IsNullOrEmpty(Format))
            {
                format = Format + ", ";
            }
            if (NumberOfPages > 0)
            {
                format += NumberOfPages.ToString() + " pages";
            }
            if (!String.IsNullOrEmpty(format))
                Details.Add(new BookDetail { Key = "Format", Value = format });

            //date published
            string published = String.Empty;
            if (!String.IsNullOrEmpty(Publisher))
            {
                published = Publisher + ", ";
            }
            if (!String.IsNullOrEmpty(book.Publication_year))
            {
                published += book.Publication_year;
            }
            if (!String.IsNullOrEmpty(published))
                Details.Add(new BookDetail { Key = "Published", Value = published });
            if (!String.IsNullOrEmpty(OriginalTitle))
                Details.Add(new BookDetail { Key = "Original Title", Value = OriginalTitle });
            if (!String.IsNullOrEmpty(Isbn13))
                Details.Add(new BookDetail { Key = "ISBN12", Value = Isbn13 });
            if (book != null && book.SeriesWorks != null && book.SeriesWorks.Series_work != null && book.SeriesWorks.Series_work.Series != null && !String.IsNullOrEmpty(book.SeriesWorks.Series_work.Series.Title))
                Details.Add(new BookDetail { Key = "Series", Value = book.SeriesWorks.Series_work.Series.Title.Replace("\n", "").Trim(' ') });

            Stats = String.Empty;
            if (!String.IsNullOrEmpty(book.Work.Ratings_count))
                Stats += book.Work.Ratings_count + " ratings, ";
            if (!String.IsNullOrEmpty(book.Work.Text_reviews_count))
                Stats += book.Work.Text_reviews_count + " reviews";
        }
    }

    public class BookDetail
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
