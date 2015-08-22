using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;
using System.Collections.ObjectModel;
using MyShelf.API.Services;

namespace MyShelf.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookImageUrlLarge { get; set; }
        public Uri BookImageUrl { get; set; } = new Uri(@"C:\Users\fmmen\Documents\Visual Studio 2015\Projects\GoodReads\MyShelf\Assets\placeholder.png");
        public string BookImageUrlSmall { get; set; }
        public string Stats { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double MyRating { get; set; }
        public ObservableCollection<Detail> Details { get; set; } = new ObservableCollection<Detail>();
        public ObservableCollection<ReviewViewModel> Reviews { get; set; } = new ObservableCollection<ReviewViewModel>();

        public BookViewModel(Book book)
        {
            BookId = book.Id;
            BookTitle = book.Title;
            BookAuthor = string.Join(", ", book.Authors.Select(a => a.Name));
            BookImageUrlLarge = book.LargeImageUrl;
            BookImageUrl = new Uri(book.ImageUrl);
            BookImageUrlSmall = book.SmallImageUrl;

            Link = book.Link;
            Description = book.Description;
        }

        public BookViewModel(string id)
        {
            GetBookInfo(id);
        }

        private async Task GetBookInfo(string id)
        {
            var book = await BookService.Instance.GetBookInfo(id);

            BookTitle = book.Title;
            BookAuthor = string.Join(", ", book.Authors.Select(a => a.Name));
            BookImageUrlLarge = book.LargeImageUrl;
            BookImageUrl = new Uri(book.ImageUrl);
            BookImageUrlSmall = book.SmallImageUrl;

            double r;
            if (double.TryParse(book.AverageRating, out r))
                Rating = r;

            Link = book.Link;
            Description = book.Description;

            OnPropertyChanged("BookTitle");
            OnPropertyChanged("BookAuthor");
            OnPropertyChanged("BookImageUrlLarge");
            OnPropertyChanged("BookImageUrl");
            OnPropertyChanged("BookImageUrlSmall");
            OnPropertyChanged("Link");
            OnPropertyChanged("Description");
            OnPropertyChanged("Rating");

            BuildBookDetailsList(book);
            OnPropertyChanged("Details");
            OnPropertyChanged("Stats");

            LoadReviews(book);
        }

        private void LoadReviews(Book book)
        {
            Reviews.Clear();
            foreach (var review in book.Reviews.Review)
            {
                Reviews.Add(new ReviewViewModel(review));
            }
            OnPropertyChanged("Reviews");
        }

        private void BuildBookDetailsList(Book book)
        {
            Details.Clear();

            #region Format
            var format = new List<string>();
            if (!string.IsNullOrEmpty(book.Format))
                format.Add(book.Format);
            if (string.IsNullOrEmpty(book.NumPages))
                format.Add($"{book.NumPages} pages");
            if (format.Count > 0)
                Details.Add(new Detail { Key = "Format", Value = string.Join(", ", format) });
            #endregion

            #region Published
            var published = new List<string>();
            if (!string.IsNullOrEmpty(book.Publisher))
                published.Add(book.Publisher);
            if (string.IsNullOrEmpty(book.PublicationYear))
                published.Add(book.PublicationYear);
            if (published.Count > 0)
                Details.Add(new Detail { Key = "Published", Value = string.Join(", ", published) });
            #endregion

            if (!string.IsNullOrEmpty(book.Work?.OriginalTitle))
                Details.Add(new Detail { Key = "Original Title", Value = book.Work.OriginalTitle });

            if (!string.IsNullOrEmpty(book.Isbn13))
                Details.Add(new Detail { Key = "ISBN13", Value = book.Isbn13 });

            if (book.SeriesWorks != null && book.SeriesWorks.Count > 0 && !string.IsNullOrEmpty(book.SeriesWorks?[0]?.Series?.Title))
                Details.Add(new Detail { Key = "Series", Value = book.SeriesWorks[0].Series.Title.Replace("\n", "").Trim(' ') });

            #region Stats
            var stats = new List<string>();
            if (!string.IsNullOrEmpty(book.Work.RatingsCount))
                stats.Add($"{book.Work.RatingsCount} ratings");
            if (!string.IsNullOrEmpty(book.Work.TextReviewsCount))
                stats.Add($"{book.Work.TextReviewsCount} reviews");
            if (stats.Count > 0)
                Stats = string.Join(", ", stats);
            #endregion
        }
    }

    public class Detail
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
