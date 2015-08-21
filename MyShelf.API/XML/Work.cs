using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{

    [XmlRoot(ElementName = "work")]
    public class Work
    {
        [XmlElement(ElementName = "books_count")]
        public string BooksCount { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "original_publication_day")]
        public string OriginalPublicationDay { get; set; }

        [XmlElement(ElementName = "original_publication_month")]
        public string OriginalPublicationMonth { get; set; }

        [XmlElement(ElementName = "original_publication_year")]
        public string OriginalPublicationYear { get; set; }

        [XmlElement(ElementName = "ratings_count")]
        public string RatingsCount { get; set; }

        [XmlElement(ElementName = "text_reviews_count")]
        public string TextReviewsCount { get; set; }

        [XmlElement(ElementName = "average_rating")]
        public string AverageRating { get; set; }

        //[XmlElement(ElementName = "best_book")]
        //public BestBook Best_book { get; set; }

        [XmlElement(ElementName = "best_book_id")]
        public string BestBookId { get; set; }

        [XmlElement(ElementName = "default_chaptering_book_id")]
        public string DefaultChapteringBookId { get; set; }

        [XmlElement(ElementName = "default_description_language_code")]
        public string DefaultDescriptionLanguageCode { get; set; }

        [XmlElement(ElementName = "desc_user_id")]
        public string DescUserId { get; set; }

        [XmlElement(ElementName = "media_type")]
        public string MediaType { get; set; }

        [XmlElement(ElementName = "original_language_id")]
        public string OriginalLanguageId { get; set; }

        [XmlElement(ElementName = "original_title")]
        public string OriginalTitle { get; set; }

        [XmlElement(ElementName = "rating_dist")]
        public string RatingDist { get; set; }

        [XmlElement(ElementName = "ratings_sum")]
        public string RatingsSum { get; set; }

        [XmlElement(ElementName = "reviews_count")]
        public string ReviewsCount { get; set; }
    }
}
