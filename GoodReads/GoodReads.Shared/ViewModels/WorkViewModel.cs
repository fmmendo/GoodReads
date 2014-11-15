using GoodReads.API;
using GoodReads.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodReads.ViewModels
{
    public class WorkViewModel
    {
        private GoodReadsAPI _gr = App.Goodreads;
        private Work work;

        public String Rating { get { return work.Average_rating; } }
        public String BooksCount { get { return work.Books_count; } }
        public String BestBookId { get { return work.Best_book_id; } }
        public String RatingsCount { get { return work.Ratings_count; } }
        public String TextReviewsCount { get { return work.Text_reviews_count; } }


        public String AuthorName { get { return work.Best_book.Author.Name; } }
        public String Id { get { return work.Best_book.Id; } }
        public String ImageURL { get { return work.Best_book.ImageUrl; } }
        public String SmallImageURL { get { return work.Best_book.SmallImageUrl; } }
        public String Title { get { return work.Best_book.Title; } }

        public WorkViewModel(Work work)
        {
            this.work = work;
        }

        private async void NewMethod(Work work)
        {
            var r2 = await _gr.GetBookInfo(work.Best_book.Id);
        }
    }
}
