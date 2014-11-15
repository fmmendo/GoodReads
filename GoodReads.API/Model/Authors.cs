using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "authors")]
    public class Authors
    {
        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }
    }
}
