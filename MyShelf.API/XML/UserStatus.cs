using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "user_status")]
    public class UserStatus
    {
        [XmlElement(ElementName = "body")]
        public string Body { get; set; }
        
        [XmlElement(ElementName = "book_id")]
        public string Book_id { get; set; }
        
        [XmlElement(ElementName = "chapter")]
        public string Chapter { get; set; }
        
        [XmlElement(ElementName = "comments_count")]
        public string Comments_count { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public string Created_at { get; set; }
        
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        
        [XmlElement(ElementName = "last_comment_at")]
        public string Last_comment_at { get; set; }
        
        [XmlElement(ElementName = "note_updated_at")]
        public string Note_updated_at { get; set; }
        
        [XmlElement(ElementName = "note_uri")]
        public string Note_uri { get; set; }
        
        [XmlElement(ElementName = "page")]
        public string Page { get; set; }
        
        [XmlElement(ElementName = "percent")]
        public string Percent { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public string Ratings_count { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public string Updated_at { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public string User_id { get; set; }
        
        [XmlElement(ElementName = "work_id")]
        public string Work_id { get; set; }
        
        [XmlElement(ElementName = "review_id")]
        public string Review_id { get; set; }
        
        [XmlElement(ElementName = "book")]
        public Book Book { get; set; }
    }
}