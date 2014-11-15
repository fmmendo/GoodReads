using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "recommendation")]
    public class Recommendation
    {
        [XmlElement(ElementName = "book_id")]
        public String Book_id { get; set; }
        
        [XmlElement(ElementName = "comments_count")]
        public String Comments_count { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "from_user_id")]
        public String From_user_id { get; set; }
        
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "last_comment_at")]
        public String Last_comment_at { get; set; }
        
        [XmlElement(ElementName = "message")]
        public String Message { get; set; }
        
        [XmlElement(ElementName = "ratings_count")]
        public String Ratings_count { get; set; }
        
        [XmlElement(ElementName = "ratings_sum")]
        public String Ratings_sum { get; set; }
        
        [XmlElement(ElementName = "recommendation_request_id")]
        public String Recommendation_request_id { get; set; }
        
        [XmlElement(ElementName = "status")]
        public String Status { get; set; }
        
        [XmlElement(ElementName = "to_user_id")]
        public String To_user_id { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
    }

}
