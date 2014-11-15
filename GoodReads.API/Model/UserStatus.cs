using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "user_status")]
    public class UserStatus
    {
        [XmlElement(ElementName = "body")]
        public String Body { get; set; }
        
        [XmlElement(ElementName = "book_id")]
        public String Book_id { get; set; }
        
        [XmlElement(ElementName = "chapter")]
        public String Chapter { get; set; }
        
        [XmlElement(ElementName = "comments_count")]
        public String Comments_count { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "last_comment_at")]
        public String Last_comment_at { get; set; }
        
        [XmlElement(ElementName = "note_updated_at")]
        public String Note_updated_at { get; set; }
        
        [XmlElement(ElementName = "note_uri")]
        public String Note_uri { get; set; }
        
        [XmlElement(ElementName = "page")]
        public String Page { get; set; }
        
        [XmlElement(ElementName = "percent")]
        public String Percent { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public String Ratings_count { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public String User_id { get; set; }
        
        [XmlElement(ElementName = "work_id")]
        public String Work_id { get; set; }
        
        [XmlElement(ElementName = "review_id")]
        public String Review_id { get; set; }
        
        [XmlElement(ElementName = "book")]
        public Book Book { get; set; }
    }
}