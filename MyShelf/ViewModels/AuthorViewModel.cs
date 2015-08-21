using Mendo.UAP.Common;
using MyShelf.API.Services;
using MyShelf.API.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        public string Id { get; private set; }
        public string ImageUrl { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Hometown { get; private set; }
        public string BornAt { get; private set; }
        public string DiedAt { get; private set ; }
        public string Genre { get; private set; }
        public string Gender { get; private set; }
        public string Link { get; private set; }
        public string About { get; private set; }
        public string Influences { get; private set; }
        public string Rating { get; private set; }
        public string WorksCount { get; private set; }
        public ObservableCollection<BookViewModel> AuthorBooks { get; private set; } = new ObservableCollection<BookViewModel>();

        public AuthorViewModel(Author author)
        {
            PopulateAuthorDetails(author);

            GetAuthorBooks(author.Id);
        }

        public AuthorViewModel(string id)
        {
            GetAuthorData(id);
        }

        private async Task GetAuthorData(string id)
        {
            var author = await AuthorService.Instance.GetAuthorInfo(id);

            PopulateAuthorDetails(author);

            GetAuthorBooks(id);
        }

        private async Task GetAuthorBooks(string id)
        {
            AuthorBooks.Clear();
            var books = await AuthorService.Instance.GetAuthorBooks(id);
            foreach (var book in books.Book)
            {
                AuthorBooks.Add(new BookViewModel(book));
            }
        }

        private void PopulateAuthorDetails(Author author)
        {
            Id = author.Id;

            ImageUrl = author.ImageUrl;
            Name = author.Name;
            Url = author.Url;
            Hometown = author.Hometown;
            BornAt = author.BornAt;
            DiedAt = author.DiedAt;
            Genre = string.Join(", ", new[] { author.Genre1, author.Genre2, author.Genre3 });
            Gender = author.Gender;
            Link = author.Link;
            About = WebUtility.HtmlDecode(author.About);
            Influences = author.Influences;
            Rating = author.AverageRating;
            WorksCount = author.WorksCount;
        }
    }
}
