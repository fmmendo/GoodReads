using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "friend")]
    public class Friend
    {
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "friend_approved_at")]
        public String Friend_approved_at { get; set; }
        
        [XmlElement(ElementName = "friend_user_id")]
        public String Friend_user_id { get; set; }
        
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "relationship")]
        public String Relationship { get; set; }
        
        [XmlElement(ElementName = "story")]
        public String Story { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
        
        [XmlElement(ElementName = "updates_flag")]
        public String Updates_flag { get; set; }
        
        [XmlElement(ElementName = "user_approved_at")]
        public String User_approved_at { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public String User_id { get; set; }
    }

}
