using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "user_shelf")]
    public class UserShelf
    {
        [XmlElement(ElementName = "book_count")]
        public string BookCount { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "display_fields")]
        public string DisplayFields { get; set; }

        [XmlElement(ElementName = "exclusive_flag")]
        public string ExclusiveFlag { get; set; }

        [XmlElement(ElementName = "featured")]
        public string Featured { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "order")]
        public string Order { get; set; }

        [XmlElement(ElementName = "per_page")]
        public string PerPage { get; set; }

        [XmlElement(ElementName = "recommend_for")]
        public string RecommendFor { get; set; }

        [XmlElement(ElementName = "sort")]
        public string Sort { get; set; }

        [XmlElement(ElementName = "sticky")]
        public string Sticky { get; set; }
    }
}
