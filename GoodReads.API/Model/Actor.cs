using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "actor")]
    public class Actor
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "name")]
        public String Name { get; set; }

        [XmlElement(ElementName = "fans_count")]
        public String Fans_count { get; set; }

        [XmlElement(ElementName = "image_url")]
        public String Image_url { get; set; }

        [XmlElement(ElementName = "about")]
        public String About { get; set; }

        [XmlElement(ElementName = "influences")]
        public String Influences { get; set; }

        [XmlElement(ElementName = "works_count")]
        public String WorksCounts{ get; set; }

        [XmlElement(ElementName = "small_image_url")]
        public String Small_image_url { get; set; }
        
        [XmlElement(ElementName = "link")]
        public String Link { get; set; }

        [XmlElement(ElementName = "average_rating")]
        public String Average_rating { get; set; }

        [XmlElement(ElementName = "ratings_count")]
        public String Ratings_count { get; set; }

        [XmlElement(ElementName = "text_reviews_count")]
        public String Text_reviews_count { get; set; }
        
        [XmlElement(ElementName = "gender")]
        public String Gender { get; set; }

        [XmlElement(ElementName = "hometown")]
        public String Hometown { get; set; }

        [XmlElement(ElementName = "born_at")]
        public String BornAt { get; set; }
        
        [XmlElement(ElementName = "died_at")]
        public String DiedAt { get; set; }
        
        [XmlElement(ElementName = "user")]
        public User User { get; set; }
        
        [XmlElement(ElementName = "books")]
        public Books Books { get; set; }
    }
}
