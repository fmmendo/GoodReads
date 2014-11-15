using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "user_challenge")]
    public class UserChallenge
    {
        [XmlElement(ElementName = "challenge_id")]
        public String Challenge_id { get; set; }
        
        [XmlElement(ElementName = "comments_count")]
        public String Comments_count { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "finished_at")]
        public String Finished_at { get; set; }
        
        [XmlElement(ElementName = "finished_flag")]
        public String Finished_flag { get; set; }
        
        [XmlElement(ElementName = "goal")]
        public String Goal { get; set; }
        
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "last_comment_at")]
        public String Last_comment_at { get; set; }
        
        [XmlElement(ElementName = "num_read")]
        public String Num_read { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public String User_id { get; set; }
        
        [XmlElement(ElementName = "user_shelf_id")]
        public String User_shelf_id { get; set; }
    }

}
