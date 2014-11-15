using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "user_shelves")]
    public class UserShelves
    {
        [XmlElement(ElementName = "user_shelf")]
        public List<UserShelf> User_shelf { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public String Type { get; set; }
    }
}