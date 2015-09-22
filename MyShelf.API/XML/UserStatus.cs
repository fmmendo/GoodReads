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
        public string BookId { get; set; }
        
        [XmlElement(ElementName = "chapter")]
        public string Chapter { get; set; }
        
        [XmlElement(ElementName = "comments_count")]
        public string CommentsCount { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public string CreatedAt { get; set; }
        
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        
        [XmlElement(ElementName = "last_comment_at")]
        public string LastCommentAt { get; set; }
        
        [XmlElement(ElementName = "note_updated_at")]
        public string NoteUpdatedSt { get; set; }
        
        [XmlElement(ElementName = "note_uri")]
        public string NoteUri { get; set; }
        
        [XmlElement(ElementName = "page")]
        public string Page { get; set; }
        
        [XmlElement(ElementName = "percent")]
        public string Percent { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public string RatingsCount { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public string UpdatedAt { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public string UserId { get; set; }
        
        [XmlElement(ElementName = "work_id")]
        public string WorkId { get; set; }
        
        [XmlElement(ElementName = "review_id")]
        public string ReviewId { get; set; }
        
        [XmlElement(ElementName = "book")]
        public Book Book { get; set; }
    }
}