using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{

    [XmlRoot(ElementName = "work")]
    public class Work
    {
        [XmlElement(ElementName = "books_count")]
        public String Books_count { get; set; }

        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "original_publication_day")]
        public String Original_publication_day { get; set; }

        [XmlElement(ElementName = "original_publication_month")]
        public String Original_publication_month { get; set; }

        [XmlElement(ElementName = "original_publication_year")]
        public String Original_publication_year { get; set; }

        [XmlElement(ElementName = "ratings_count")]
        public String Ratings_count { get; set; }

        [XmlElement(ElementName = "text_reviews_count")]
        public String Text_reviews_count { get; set; }

        [XmlElement(ElementName = "average_rating")]
        public String Average_rating { get; set; }

        [XmlElement(ElementName = "best_book")]
        public BestBook Best_book { get; set; }

        [XmlElement(ElementName = "best_book_id")]
        public String Best_book_id { get; set; }

        [XmlElement(ElementName = "default_chaptering_book_id")]
        public String Default_chaptering_book_id { get; set; }

        [XmlElement(ElementName = "default_description_language_code")]
        public String Default_description_language_code { get; set; }

        [XmlElement(ElementName = "desc_user_id")]
        public String Desc_user_id { get; set; }

        [XmlElement(ElementName = "media_type")]
        public String Media_type { get; set; }

        [XmlElement(ElementName = "original_language_id")]
        public String Original_language_id { get; set; }

        [XmlElement(ElementName = "original_title")]
        public String Original_title { get; set; }

        [XmlElement(ElementName = "rating_dist")]
        public String Rating_dist { get; set; }

        [XmlElement(ElementName = "ratings_sum")]
        public String Ratings_sum { get; set; }

        [XmlElement(ElementName = "reviews_count")]
        public String Reviews_count { get; set; }
    }
}
