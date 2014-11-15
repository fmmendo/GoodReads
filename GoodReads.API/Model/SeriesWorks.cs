using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "series_works")]
    public class SeriesWorks
    {
        [XmlElement(ElementName = "series_work")]
        public SeriesWork Series_work { get; set; }
    }
}
