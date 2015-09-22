using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "friends")]
    public class Friends
    {
        [XmlElement(ElementName = "user")]
        public List<User> User { get; set; }

        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }

        [XmlAttribute(AttributeName = "end")]
        public string End { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
    }
}
