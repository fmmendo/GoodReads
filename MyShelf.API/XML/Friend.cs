using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "friend")]
    public class Friend
    {
        [XmlElement(ElementName = "created_at")]
        public string Created_at { get; set; }
        
        [XmlElement(ElementName = "friend_approved_at")]
        public string Friend_approved_at { get; set; }
        
        [XmlElement(ElementName = "friend_user_id")]
        public string Friend_user_id { get; set; }
        
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        
        [XmlElement(ElementName = "relationship")]
        public string Relationship { get; set; }
        
        [XmlElement(ElementName = "story")]
        public string Story { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public string Updated_at { get; set; }
        
        [XmlElement(ElementName = "updates_flag")]
        public string Updates_flag { get; set; }
        
        [XmlElement(ElementName = "user_approved_at")]
        public string User_approved_at { get; set; }
        
        [XmlElement(ElementName = "user_id")]
        public string User_id { get; set; }
    }

}
