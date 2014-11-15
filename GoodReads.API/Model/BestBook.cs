using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "best_book")]
    public class BestBook
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "title")]
        public String Title { get; set; }
        
        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public String ImageUrl { get; set; }
        
        [XmlElement(ElementName = "small_image_url")]
        public String SmallImageUrl { get; set; }
        
        [XmlAttribute(AttributeName = "type")]
        public String Type { get; set; }
    }
}
