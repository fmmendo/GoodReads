using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlAttribute(AttributeName = "id")]
        public String Id;

        [XmlElement(ElementName = "name")]
        public String Name;

        [XmlElement(ElementName = "user_name")]
        public String User_name;

        [XmlElement(ElementName = "link")]
        public String Link;
        
        [XmlElement(ElementName = "image_url")]
        public String Image_url;
        
        [XmlElement(ElementName = "small_image_url")]
        public String Small_image_url;
        
        [XmlElement(ElementName = "about")]
        public String About;
        
        [XmlElement(ElementName = "age")]
        public String Age;
        
        [XmlElement(ElementName = "gender")]
        public String Gender;
        
        [XmlElement(ElementName = "location")]
        public String Location;
        
        [XmlElement(ElementName = "website")]
        public String Website;
        
        [XmlElement(ElementName = "joined")]
        public String Joined;
        
        [XmlElement(ElementName = "last_active")]
        public String Last_active;
        
        [XmlElement(ElementName = "interests")]
        public String Interests;
        
        [XmlElement(ElementName = "favorite_books")]
        public String Favorite_books;
        
        [XmlElement(ElementName = "favorite_authors")]
        public Authors Favorite_authors;
        
        [XmlElement(ElementName = "updates_rss_url")]
        public String Updates_rss_url;
        
        [XmlElement(ElementName = "reviews_rss_url")]
        public String Reviews_rss_url;
        
        [XmlElement(ElementName = "friends_count")]
        public String Friends_count;
        
        [XmlElement(ElementName = "groups_count")]
        public String Groups_count;
        
        [XmlElement(ElementName = "reviews_count")]
        public String Reviews_count;
        
        [XmlElement(ElementName = "user_shelves")]
        public UserShelves User_shelves;
        
        [XmlElement(ElementName = "updates")]
        public Updates Updates;
        
        [XmlElement(ElementName = "user_statuses")]
        public UserStatuses User_statuses;
    }
}
