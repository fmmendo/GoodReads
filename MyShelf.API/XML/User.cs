using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id;

        [XmlElement(ElementName = "name")]
        public string Name;

        [XmlElement(ElementName = "user_name")]
        public string User_name;

        [XmlElement(ElementName = "link")]
        public string Link;
        
        [XmlElement(ElementName = "image_url")]
        public string Image_url;
        
        [XmlElement(ElementName = "small_image_url")]
        public string Small_image_url;
        
        [XmlElement(ElementName = "about")]
        public string About;
        
        [XmlElement(ElementName = "age")]
        public string Age;
        
        [XmlElement(ElementName = "gender")]
        public string Gender;
        
        [XmlElement(ElementName = "location")]
        public string Location;
        
        [XmlElement(ElementName = "website")]
        public string Website;
        
        [XmlElement(ElementName = "joined")]
        public string Joined;
        
        [XmlElement(ElementName = "last_active")]
        public string Last_active;
        
        [XmlElement(ElementName = "interests")]
        public string Interests;
        
        [XmlElement(ElementName = "favorite_books")]
        public string Favorite_books;

        [XmlArray("favorite_authors")]
        [XmlArrayItem("favorite_author")]
        public List<Author> Favorite_authors;
        
        [XmlElement(ElementName = "updates_rss_url")]
        public string Updates_rss_url;
        
        [XmlElement(ElementName = "reviews_rss_url")]
        public string Reviews_rss_url;
        
        [XmlElement(ElementName = "friends_count")]
        public string Friends_count;
        
        [XmlElement(ElementName = "groups_count")]
        public string Groups_count;
        
        [XmlElement(ElementName = "reviews_count")]
        public string Reviews_count;
        
        //[XmlElement(ElementName = "user_shelves")]
        //public UserShelves User_shelves;
        
        [XmlElement(ElementName = "updates")]
        public Updates Updates;
        
        //[XmlElement(ElementName = "user_statuses")]
        //public UserStatuses User_statuses;
    }
}
