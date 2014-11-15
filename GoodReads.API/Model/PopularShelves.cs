using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "popular_shelves")]
    public class PopularShelves
    {
        [XmlElement(ElementName = "shelf")]
        public List<Shelf> Shelf { get; set; }
    }
}
