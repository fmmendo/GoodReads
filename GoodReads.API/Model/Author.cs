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
        public String Author_program_at { get; set; }
        
        [XmlElement(ElementName = "best_book_id")]
        public String Best_book_id { get; set; }
        
        [XmlElement(ElementName = "books_count")]
        public String Books_count { get; set; }
        
        [XmlElement(ElementName = "born_at")]
        public String Born_at { get; set; }
        
        [XmlElement(ElementName = "country_code")]
        public String Country_code { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "died_at")]
        public String Died_at { get; set; }
        
        [XmlElement(ElementName = "fanships_count")]
        public String Fanships_count { get; set; }
        
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
        public String Image_copyright { get; set; }
        
        [XmlElement(ElementName = "image_uploaded_at")]
        public String Image_uploaded_at { get; set; }
        
        [XmlElement(ElementName = "influences")]
        public String Influences { get; set; }
        
        [XmlElement(ElementName = "name")]
        public String Name { get; set; }
        
        [XmlElement(ElementName = "name_language_code")]
        public String Name_language_code { get; set; }
        
        [XmlElement(ElementName = "postal_code")]
        public String Postal_code { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public String Ratings_count { get; set; }
        
        [XmlElement(ElementName = "ratings_sum")]
        public String Ratings_sum { get; set; }
        
        [XmlElement(ElementName = "reviews_count")]
        public String Reviews_count { get; set; }
        
        [XmlElement(ElementName = "s3_image_at")]
        public String S3_image_at { get; set; }
        
        [XmlElement(ElementName = "searched_for_at")]
        public String Searched_for_at { get; set; }
        
        [XmlElement(ElementName = "text_reviews_count")]
        public String Text_reviews_count { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
        
        [XmlElement(ElementName = "uploader_user_id")]
        public String Uploader_user_id { get; set; }
        
        [XmlElement(ElementName = "url")]
        public String Url { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public String User_id { get; set; }
        
        [XmlElement(ElementName = "works_count")]
        public String Works_count { get; set; }
        
        [XmlElement(ElementName = "rating_dist")]
        public String Rating_dist { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public String Image_url { get; set; }
        
        [XmlElement(ElementName = "small_image_url")]
        public String Small_image_url { get; set; }
        
        [XmlElement(ElementName = "link")]
        public String Link { get; set; }
        
        [XmlElement(ElementName = "average_rating")]
        public String Average_rating { get; set; }

        [XmlElement(ElementName = "books")]
        public Books Books { get; set; }
    }
}
