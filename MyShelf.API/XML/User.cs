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
        public string UserName;

        [XmlElement(ElementName = "link")]
        public string Link;
        
        [XmlElement(ElementName = "image_url")]
        public string ImageUrl;
        
        [XmlElement(ElementName = "small_image_url")]
        public string SmallImageUrl;
        
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
        public string LastActive;
        
        [XmlElement(ElementName = "interests")]
        public string Interests;
        
        [XmlElement(ElementName = "favorite_books")]
        public string FavoriteBooks;

        [XmlArray("favorite_authors")]
        [XmlArrayItem("favorite_author")]
        public List<Author> FavoriteAuthors;
        
        [XmlElement(ElementName = "updates_rss_url")]
        public string UpdatesRssUrl;
        
        [XmlElement(ElementName = "reviews_rss_url")]
        public string ReviewsRssUrl;
        
        [XmlElement(ElementName = "friends_count")]
        public string FriendsCount;
        
        [XmlElement(ElementName = "groups_count")]
        public string GroupsCount;
        
        [XmlElement(ElementName = "reviews_count")]
        public string ReviewsCount;
        
        //[XmlElement(ElementName = "user_shelves")]
        //public UserShelves User_shelves;
        
        [XmlElement(ElementName = "updates")]
        public Updates Updates;

        //[XmlElement(ElementName = "user_statuses")]
        [XmlArray("user_statuses")]
        [XmlArrayItem("user_status")]
        public List<UserStatus> UserStatuses;
    }
}
