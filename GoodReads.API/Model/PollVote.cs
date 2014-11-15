using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "poll_vote")]
    public class PollVote
    {
        [XmlElement(ElementName = "comments_count")]
        public String Comments_count { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "last_comment_at")]
        public String Last_comment_at { get; set; }
        
        [XmlElement(ElementName = "poll_answer_id")]
        public String Poll_answer_id { get; set; }
        
        [XmlElement(ElementName = "poll_id")]
        public String Poll_id { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public String User_id { get; set; }
    }

}
