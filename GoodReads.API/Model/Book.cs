using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "book")]
    public class Book
    {
        [XmlElement(ElementName = "asin")]
        public String Asin { get; set; }

        [XmlElement(ElementName = "asin_updater_user_id")]
        public String Asin_updater_user_id { get; set; }

        [XmlElement(ElementName = "author_id")]
        public String Author_id { get; set; }

        [XmlElement(ElementName = "author_id_updater_user_id")]
        public String Author_id_updater_user_id { get; set; }

        [XmlElement(ElementName = "author_role")]
        public String Author_role { get; set; }

        [XmlElement(ElementName = "author_role_updater_user_id")]
        public String Author_role_updater_user_id { get; set; }

        [XmlElement(ElementName = "book_authors_count")]
        public String Book_authors_count { get; set; }

        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }

        [XmlElement(ElementName = "description_language_code")]
        public String Description_language_code { get; set; }

        [XmlElement(ElementName = "description_updater_user_id")]
        public String Description_updater_user_id { get; set; }

        [XmlElement(ElementName = "edition_information")]
        public String Edition_information { get; set; }

        [XmlElement(ElementName = "edition_information_updater_user_id")]
        public String Edition_information_updater_user_id { get; set; }

        [XmlElement(ElementName = "format_updater_user_id")]
        public String Format_updater_user_id { get; set; }

        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "image_updater_user_id")]
        public String Image_updater_user_id { get; set; }

        [XmlElement(ElementName = "image_uploaded_at")]
        public String Image_uploaded_at { get; set; }

        [XmlElement(ElementName = "isbn13_updater_user_id")]
        public String Isbn13_updater_user_id { get; set; }

        [XmlElement(ElementName = "isbn_updater_user_id")]
        public String Isbn_updater_user_id { get; set; }

        [XmlElement(ElementName = "is_ebook")]
        public String IsEbook { get; set; }

        [XmlElement(ElementName = "language_code")]
        public String Language_code { get; set; }

        [XmlElement(ElementName = "language_updater_user_id")]
        public String Language_updater_user_id { get; set; }

        [XmlElement(ElementName = "num_pages")]
        public String Num_pages { get; set; }

        [XmlElement(ElementName = "num_pages_updater_user_id")]
        public String Num_pages_updater_user_id { get; set; }

        [XmlElement(ElementName = "publication_date_updater_user_id")]
        public String Publication_date_updater_user_id { get; set; }

        [XmlElement(ElementName = "publication_day")]
        public String Publication_day { get; set; }

        [XmlElement(ElementName = "publication_month")]
        public String Publication_month { get; set; }

        [XmlElement(ElementName = "publication_year")]
        public String Publication_year { get; set; }

        [XmlElement(ElementName = "publisher")]
        public String Publisher { get; set; }

        [XmlElement(ElementName = "publisher_language_code")]
        public String Publisher_language_code { get; set; }

        [XmlElement(ElementName = "publisher_updater_user_id")]
        public String Publisher_updater_user_id { get; set; }

        [XmlElement(ElementName = "ratings_count")]
        public String Ratings_count { get; set; }

        [XmlElement(ElementName = "ratings_sum")]
        public String Ratings_sum { get; set; }

        [XmlElement(ElementName = "reviews_count")]
        public String Reviews_count { get; set; }

        [XmlElement(ElementName = "s3_image_at")]
        public String S3_image_at { get; set; }

        [XmlElement(ElementName = "sort_by_title")]
        public String Sort_by_title { get; set; }

        [XmlElement(ElementName = "source_url")]
        public String Source_url { get; set; }

        [XmlElement(ElementName = "text_reviews_count")]
        public String Text_reviews_count { get; set; }

        [XmlElement(ElementName = "title")]
        public String Title { get; set; }

        [XmlElement(ElementName = "title_language_code")]
        public String Title_language_code { get; set; }

        [XmlElement(ElementName = "title_updater_user_id")]
        public String Title_updater_user_id { get; set; }

        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }

        [XmlElement(ElementName = "url")]
        public String Url { get; set; }

        [XmlElement(ElementName = "url_updater_user_id")]
        public String Url_updater_user_id { get; set; }

        [XmlElement(ElementName = "work_id")]
        public String Work_id { get; set; }

        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }

        [XmlElement(ElementName = "description")]
        public String Description { get; set; }

        [XmlElement(ElementName = "isbn")]
        public String Isbn { get; set; }

        [XmlElement(ElementName = "isbn13")]
        public String Isbn13 { get; set; }

        [XmlElement(ElementName = "link")]
        public String Link { get; set; }

        [XmlElement(ElementName = "authors")]
        public Authors Authors { get; set; }

        [XmlElement(ElementName = "reviews")]
        public Reviews Reviews { get; set; }

        [XmlElement(ElementName = "format")]
        public String Format { get; set; }

        [XmlElement(ElementName = "image_url")]
        public String Image_url { get; set; }

        [XmlElement(ElementName = "small_image_url")]
        public String Small_image_url { get; set; }

        [XmlElement(ElementName = "large_image_url")]
        public String Large_image_url { get; set; }

        [XmlElement(ElementName = "average_rating")]
        public String Average_rating { get; set; }

        [XmlElement(ElementName = "published")]
        public String Published { get; set; }

        [XmlElement(ElementName = "reviews_widget")]
        public String ReviewsWidget { get; set; }

        [XmlElement(ElementName = "popular_shelves")]
        public PopularShelves PopularShelves { get; set; }

        [XmlElement(ElementName = "book_links")]
        public BookLinks BookLinks { get; set; }

        [XmlElement(ElementName = "series_works")]
        public SeriesWorks SeriesWorks { get; set; }

        [XmlElement(ElementName = "similar_books")]
        public SimilarBooks SimilarBooks { get; set; }

        [XmlElement(ElementName = "work")]
        public Work Work { get; set; }
    }
}
