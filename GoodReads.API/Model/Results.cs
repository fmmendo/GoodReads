using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "results")]
    public class Results
    {
        [XmlElement(ElementName = "work")]
        public List<Work> Work { get; set; }
    }
}
