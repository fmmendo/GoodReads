using Mendo.UWP.Common;
using MyShelf.API.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class WorkViewModel : ViewModelBase
    {
        public String Rating { get; set; }
        public String BooksCount { get; set; }
        public String BestBookId { get; set; }
        public String RatingsCount  { get; set; }
        public String TextReviewsCount { get; set; }
        public String AuthorName { get; set; }
        public String Id { get; set; }
        public Uri ImageURL { get; set; }
        public Uri SmallImageURL { get; set; }
        public String Title { get; set; }
        public bool IsTitleVisible { get; set; }

        public WorkViewModel(Work work)
        {
            Rating = work.AverageRating;
            BooksCount = work.BooksCount;
            BestBookId = work?.BestBook?.Id;
            RatingsCount = work.RatingsCount;
            TextReviewsCount = work.TextReviewsCount;
            //AuthorName = work.BestBook.Authors?.FirstOrDefault().Name;
            Id = work.Id;
            ImageURL = new Uri(work.BestBook.ImageUrl);
            SmallImageURL = new Uri(work.BestBook.SmallImageUrl);
            Title = work.BestBook.Title;


            IsTitleVisible = work.BestBook.ImageUrl.Contains("nophoto");
        }
    }
}
