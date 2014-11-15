using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "user_statuses")]
    public class UserStatuses
    {
        [XmlElement(ElementName = "user_status")]
        public List<UserStatus> User_status;
    }
}
