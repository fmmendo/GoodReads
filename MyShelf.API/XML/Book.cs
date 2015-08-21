using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "book")]
    public class Book
    {
        [XmlElement(ElementName = "asin")]
        public string Asin { get; set; }

        [XmlElement(ElementName = "asin_updater_user_id")]
        public string AsinUpdaterUserId { get; set; }

        [XmlElement(ElementName = "author_id")]
        public string AuthorId { get; set; }

        [XmlElement(ElementName = "author_id_updater_user_id")]
        public string AuthorIdUpdaterUserId { get; set; }

        [XmlElement(ElementName = "author_role")]
        public string AuthorRole { get; set; }

        [XmlElement(ElementName = "author_role_updater_user_id")]
        public string AuthorRoleUpdaterUserId { get; set; }

        [XmlElement(ElementName = "book_authors_count")]
        public string BookAuthorsCount { get; set; }

        [XmlElement(ElementName = "created_at")]
        public string CreatedAt { get; set; }

        [XmlElement(ElementName = "description_language_code")]
        public string DescriptionLanguageCode { get; set; }

        [XmlElement(ElementName = "description_updater_user_id")]
        public string DescriptionUpdaterUserId { get; set; }

        [XmlElement(ElementName = "edition_information")]
        public string EditionInformation { get; set; }

        [XmlElement(ElementName = "edition_information_updater_user_id")]
        public string EditionInformationUpdaterUserId { get; set; }

        [XmlElement(ElementName = "format_updater_user_id")]
        public string FormatUpdaterUserId { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "image_updater_user_id")]
        public string ImageUpdaterUserId { get; set; }

        [XmlElement(ElementName = "image_uploaded_at")]
        public string ImageUploadedAt { get; set; }

        [XmlElement(ElementName = "isbn13_updater_user_id")]
        public string Isbn13UpdaterUserId { get; set; }

        [XmlElement(ElementName = "isbn_updater_user_id")]
        public string IsbnUpdaterUserId { get; set; }

        [XmlElement(ElementName = "is_ebook")]
        public string IsEbook { get; set; }

        [XmlElement(ElementName = "language_code")]
        public string LanguageCode { get; set; }

        [XmlElement(ElementName = "language_updater_user_id")]
        public string LanguageUpdaterUserId { get; set; }

        [XmlElement(ElementName = "num_pages")]
        public string NumPages { get; set; }

        [XmlElement(ElementName = "num_pages_updater_user_id")]
        public string NumPagesUpdaterUserIid { get; set; }

        [XmlElement(ElementName = "publication_date_updater_user_id")]
        public string PublicationDateUupdaterUserId { get; set; }

        [XmlElement(ElementName = "publication_day")]
        public string PublicationDay { get; set; }

        [XmlElement(ElementName = "publication_month")]
        public string PublicationMonth { get; set; }

        [XmlElement(ElementName = "publication_year")]
        public string PublicationYear { get; set; }

        [XmlElement(ElementName = "publisher")]
        public string Publisher { get; set; }

        [XmlElement(ElementName = "publisher_language_code")]
        public string PublisherLanguageCode { get; set; }

        [XmlElement(ElementName = "publisher_updater_user_id")]
        public string Publisher_updaterUserId { get; set; }

        [XmlElement(ElementName = "ratings_count")]
        public string RatingsCount { get; set; }

        [XmlElement(ElementName = "ratings_sum")]
        public string RatingsSum { get; set; }

        [XmlElement(ElementName = "reviews_count")]
        public string ReviewsCount { get; set; }

        [XmlElement(ElementName = "s3_image_at")]
        public string S3ImageAt { get; set; }

        [XmlElement(ElementName = "sort_by_title")]
        public string SortByTitle { get; set; }

        [XmlElement(ElementName = "source_url")]
        public string SourceUrl { get; set; }

        [XmlElement(ElementName = "text_reviews_count")]
        public string TextReviewsCount { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "title_language_code")]
        public string TitleLanguageCode { get; set; }

        [XmlElement(ElementName = "title_updater_user_id")]
        public string TitleUpdaterUserId { get; set; }

        [XmlElement(ElementName = "updated_at")]
        public string UpdatedAt { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "url_updater_user_id")]
        public string UrlUpdaterUserId { get; set; }

        [XmlElement(ElementName = "work_id")]
        public string WorkId { get; set; }

        //[XmlElement(ElementName = "author")]
        //public Author Author { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "isbn")]
        public string Isbn { get; set; }

        [XmlElement(ElementName = "isbn13")]
        public string Isbn13 { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlArray("authors")]
        [XmlArrayItem("author")]
        public List<Author> Authors { get; set; }

        [XmlElement(ElementName = "reviews")]
        public Reviews Reviews { get; set; }

        [XmlElement(ElementName = "format")]
        public string Format { get; set; }

        [XmlElement(ElementName = "image_url")]
        public string ImageUrl { get; set; }

        [XmlElement(ElementName = "small_image_url")]
        public string SmallImageUrl { get; set; }

        [XmlElement(ElementName = "large_image_url")]
        public string LargeImageUrl { get; set; }

        [XmlElement(ElementName = "average_rating")]
        public string AverageRating { get; set; }

        [XmlElement(ElementName = "published")]
        public string Published { get; set; }

        [XmlElement(ElementName = "reviews_widget")]
        public string ReviewsWidget { get; set; }

        [XmlArray("popular_shelves")]
        [XmlArrayItem("shelf")]
        public List<Shelf> PopularShelves { get; set; }

        [XmlArray("book_links")]
        [XmlArrayItem("book_link")]
        public List<BookLink> BookLinks { get; set; }

        [XmlArray("series_works")]
        [XmlArrayItem("series_work")]
        public List<SeriesWork> SeriesWorks { get; set; }

        [XmlArray("similar_books")]
        [XmlArrayItem("book")]
        public List<Book> SimilarBooks { get; set; }

        [XmlElement(ElementName = "work")]
        public Work Work { get; set; }
    }
}
