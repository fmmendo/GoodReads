using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{

    [XmlRoot(ElementName = "work")]
    public class Work
    {
        [XmlElement(ElementName = "books_count")]
        public string Books_count { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "original_publication_day")]
        public string Original_publication_day { get; set; }

        [XmlElement(ElementName = "original_publication_month")]
        public string Original_publication_month { get; set; }

        [XmlElement(ElementName = "original_publication_year")]
        public string Original_publication_year { get; set; }

        [XmlElement(ElementName = "ratings_count")]
        public string Ratings_count { get; set; }

        [XmlElement(ElementName = "text_reviews_count")]
        public string Text_reviews_count { get; set; }

        [XmlElement(ElementName = "average_rating")]
        public string Average_rating { get; set; }

        //[XmlElement(ElementName = "best_book")]
        //public BestBook Best_book { get; set; }

        [XmlElement(ElementName = "best_book_id")]
        public string Best_book_id { get; set; }

        [XmlElement(ElementName = "default_chaptering_book_id")]
        public string Default_chaptering_book_id { get; set; }

        [XmlElement(ElementName = "default_description_language_code")]
        public string Default_description_language_code { get; set; }

        [XmlElement(ElementName = "desc_user_id")]
        public string Desc_user_id { get; set; }

        [XmlElement(ElementName = "media_type")]
        public string Media_type { get; set; }

        [XmlElement(ElementName = "original_language_id")]
        public string Original_language_id { get; set; }

        [XmlElement(ElementName = "original_title")]
        public string Original_title { get; set; }

        [XmlElement(ElementName = "rating_dist")]
        public string Rating_dist { get; set; }

        [XmlElement(ElementName = "ratings_sum")]
        public string Ratings_sum { get; set; }

        [XmlElement(ElementName = "reviews_count")]
        public string Reviews_count { get; set; }
    }
}
