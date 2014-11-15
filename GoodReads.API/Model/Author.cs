using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "author")]
    public class Author
    {
        [XmlElement(ElementName = "about")]
        public String About { get; set; }
        
        [XmlElement(ElementName = "author_program_at")]
        public String AuthorProgramAt { get; set; }
        
        [XmlElement(ElementName = "best_book_id")]
        public String BestBookId { get; set; }
        
        [XmlElement(ElementName = "books_count")]
        public String BooksCount { get; set; }
        
        [XmlElement(ElementName = "born_at")]
        public String BornAt { get; set; }
        
        [XmlElement(ElementName = "country_code")]
        public String CountryCode { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String CreatedAt { get; set; }
        
        [XmlElement(ElementName = "died_at")]
        public String DiedAt { get; set; }
        
        [XmlElement(ElementName = "fanships_count")]
        public String FanshipsCount { get; set; }
        
        [XmlElement(ElementName = "gender")]
        public String Gender { get; set; }
        
        [XmlElement(ElementName = "genre1")]
        public String Genre1 { get; set; }
        
        [XmlElement(ElementName = "genre2")]
        public String Genre2 { get; set; }
        
        [XmlElement(ElementName = "genre3")]
        public String Genre3 { get; set; }
        
        [XmlElement(ElementName = "hometown")]
        public String Hometown { get; set; }
        
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "image_copyright")]
        public String ImageCopyright { get; set; }
        
        [XmlElement(ElementName = "image_uploaded_at")]
        public String ImageUploadedAt { get; set; }
        
        [XmlElement(ElementName = "influences")]
        public String Influences { get; set; }
        
        [XmlElement(ElementName = "name")]
        public String Name { get; set; }
        
        [XmlElement(ElementName = "name_language_code")]
        public String NameLanguageCode { get; set; }
        
        [XmlElement(ElementName = "postal_code")]
        public String PostalCode { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public String RatingsCount { get; set; }
        
        [XmlElement(ElementName = "ratings_sum")]
        public String RatingsSum { get; set; }
        
        [XmlElement(ElementName = "reviews_count")]
        public String ReviewsCount { get; set; }
        
        [XmlElement(ElementName = "s3_image_at")]
        public String S3ImageAt { get; set; }
        
        [XmlElement(ElementName = "searched_for_at")]
        public String SearchedForAt { get; set; }
        
        [XmlElement(ElementName = "text_reviews_count")]
        public String TextReviewsCount { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String UpdatedAt { get; set; }
        
        [XmlElement(ElementName = "uploader_user_id")]
        public String Uploader_user_id { get; set; }
        
        [XmlElement(ElementName = "url")]
        public String Url { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public String UserId { get; set; }
        
        [XmlElement(ElementName = "works_count")]
        public String WorksCount { get; set; }
        
        [XmlElement(ElementName = "rating_dist")]
        public String RatingDist { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public String ImageUrl { get; set; }
        
        [XmlElement(ElementName = "small_image_url")]
        public String SmallImageUrl { get; set; }
        
        [XmlElement(ElementName = "link")]
        public String Link { get; set; }
        
        [XmlElement(ElementName = "average_rating")]
        public String AverageRating { get; set; }

        [XmlElement(ElementName = "books")]
        public Books Books { get; set; }
    }
}
