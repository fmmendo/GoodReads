using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "user_shelf")]
    public class UserShelf
    {
        [XmlElement(ElementName = "book_count")]
        public String Book_count { get; set; }

        [XmlElement(ElementName = "description")]
        public String Description { get; set; }

        [XmlElement(ElementName = "display_fields")]
        public String Display_fields { get; set; }

        [XmlElement(ElementName = "exclusive_flag")]
        public String Exclusive_flag { get; set; }

        [XmlElement(ElementName = "featured")]
        public String Featured { get; set; }

        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "name")]
        public String Name { get; set; }

        [XmlElement(ElementName = "order")]
        public String Order { get; set; }

        [XmlElement(ElementName = "per_page")]
        public String Per_page { get; set; }

        [XmlElement(ElementName = "recommend_for")]
        public String Recommend_for { get; set; }

        [XmlElement(ElementName = "sort")]
        public String Sort { get; set; }

        [XmlElement(ElementName = "sticky")]
        public String Sticky { get; set; }
    }
}
