using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "author")]
    public class Author
    {
        [XmlElement(ElementName = "about")]
        public string About { get; set; }
        
        [XmlElement(ElementName = "author_program_at")]
        public string AuthorProgramAt { get; set; }
        
        [XmlElement(ElementName = "best_book_id")]
        public string BestBookId { get; set; }
        
        [XmlElement(ElementName = "books_count")]
        public string BooksCount { get; set; }
        
        [XmlElement(ElementName = "born_at")]
        public string BornAt { get; set; }
        
        [XmlElement(ElementName = "country_code")]
        public string CountryCode { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public string CreatedAt { get; set; }
        
        [XmlElement(ElementName = "died_at")]
        public string DiedAt { get; set; }
        
        [XmlElement(ElementName = "fanships_count")]
        public string FanshipsCount { get; set; }
        
        [XmlElement(ElementName = "gender")]
        public string Gender { get; set; }
        
        [XmlElement(ElementName = "genre1")]
        public string Genre1 { get; set; }
        
        [XmlElement(ElementName = "genre2")]
        public string Genre2 { get; set; }
        
        [XmlElement(ElementName = "genre3")]
        public string Genre3 { get; set; }
        
        [XmlElement(ElementName = "hometown")]
        public string Hometown { get; set; }
        
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        
        [XmlElement(ElementName = "image_copyright")]
        public string ImageCopyright { get; set; }
        
        [XmlElement(ElementName = "image_uploaded_at")]
        public string ImageUploadedAt { get; set; }
        
        [XmlElement(ElementName = "influences")]
        public string Influences { get; set; }
        
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        
        [XmlElement(ElementName = "name_language_code")]
        public string NameLanguageCode { get; set; }
        
        [XmlElement(ElementName = "postal_code")]
        public string PostalCode { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public string RatingsCount { get; set; }
        
        [XmlElement(ElementName = "ratings_sum")]
        public string RatingsSum { get; set; }
        
        [XmlElement(ElementName = "reviews_count")]
        public string ReviewsCount { get; set; }
        
        [XmlElement(ElementName = "s3_image_at")]
        public string S3ImageAt { get; set; }
        
        [XmlElement(ElementName = "searched_for_at")]
        public string SearchedForAt { get; set; }
        
        [XmlElement(ElementName = "text_reviews_count")]
        public string TextReviewsCount { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public string UpdatedAt { get; set; }
        
        [XmlElement(ElementName = "uploader_user_id")]
        public string Uploader_user_id { get; set; }
        
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public string UserId { get; set; }
        
        [XmlElement(ElementName = "works_count")]
        public string WorksCount { get; set; }
        
        [XmlElement(ElementName = "rating_dist")]
        public string RatingDist { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public string ImageUrl { get; set; }
        
        [XmlElement(ElementName = "small_image_url")]
        public string SmallImageUrl { get; set; }
        
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName = "average_rating")]
        public string AverageRating { get; set; }

        [XmlElement(ElementName = "books")]
        public Books Books { get; set; }
    }
}
