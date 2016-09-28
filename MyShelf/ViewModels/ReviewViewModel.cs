using Mendo.UWP.Common;
using MyShelf.API.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Html;

namespace MyShelf.ViewModels
{
    public class ReviewViewModel : ViewModelBase
    {
        public String Id { get; set; }
        //public String BookId { get; set; }
        public String Body { get; set; }
        public String UserName { get; set; }
        public String UserImageUrl { get; set; }
        public double Rating { get; set; }
        public String DateAdded { get; set; }

        public ReviewViewModel(Review review)
        {
            Id = review.Id;
            //BookId = review.Book.Id;
            Body = HtmlUtilities.ConvertToText(review.Body);
            UserName = review.User.Name;
            UserImageUrl = review.User.ImageUrl;
            Rating = double.Parse(review.Rating);

            var split = review.DateAdded.Split(new[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);

            var dayofweek = split[0];
            var month = split[1];
            var day = split[2];
            var hour = (Int32.Parse(split[3]) + Int32.Parse(split[6]) / 100).ToString();
            var minute = split[4];
            var second = split[5];
            var year = split[7];

            DateAdded = String.Format("{0}, {1} {2}, {3}"/*, {4:D2}:{5:D2}:{6:D2}"*/, dayofweek, month, day, year/*, h, m, s*/);
        }
    }
}
