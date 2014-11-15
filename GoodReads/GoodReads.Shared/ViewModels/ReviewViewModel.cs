using GoodReads.API.Model;
using GoodReads.Common;
using System;
using System.Text.RegularExpressions;
using Windows.Data.Html;

namespace GoodReads.ViewModels
{
    public class ReviewViewModel : ViewModelBase
    {
        private Review review;

        private BookViewModel book;
        /// <summary>
        /// 
        /// </summary>
        public BookViewModel Book
        {
            get { return book; }
            set { book = value; }
        }

        public String Id { get { return review.Id; } }
        public String BookId { get { return Book.Id; } }
        public String Body { get { return HtmlUtilities.ConvertToText(review.Body); } }
        public String UserName { get { return review.User.Name; } }
        public String UserImageUrl { get { return review.User.Image_url; } }
        public double Rating { get { return Double.Parse(review.Rating); } }
        public String DateAdded
        {
            get
            {
                var split = review.DateAdded.Split(new[] {" ", ":"}, StringSplitOptions.RemoveEmptyEntries);

                var dow = split[0];
                var mth = split[1];
                var d = split[2];
                var h = (Int32.Parse(split[3]) + Int32.Parse(split[6]) / 100).ToString();
                var m = split[4];
                var s = split[5];
                var y = split[7];

                return String.Format("{0}, {1} {2}, {3}"/*, {4:D2}:{5:D2}:{6:D2}"*/, dow, mth, d, y/*, h, m, s*/);
            }
        }
        public String ShelfName
        {
            get
            {
                if (review.Shelves != null && review.Shelves.Shelf != null)
                    return review.Shelves.Shelf.Name ?? String.Empty;
                else
                    return String.Empty;
            }
        }

        public ReviewViewModel(Review review)
        {
            this.review = review;
            this.book = new BookViewModel(review.Book);
        }
    }
}
