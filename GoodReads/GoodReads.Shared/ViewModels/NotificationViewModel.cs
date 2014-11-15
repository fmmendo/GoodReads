using GoodReads.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodReads.ViewModels
{
    public class NotificationViewModel
    {
        Notification notification;

        public String Id { get { return notification.Id; } }

        public Actors Actors { get { return notification.Actors; } }

        public bool New { get { return Boolean.Parse(notification.New); } }

        public String ImageURL { get { return notification.Actors.User.Image_url; } }

        //public String CreatedAt { get; }

        public String Body { get { return notification.Body.Text; } }

        public String Html { get { return notification.Body.Html; } }

        public String Url { get { return notification.Url; } }

        //public String Resource_type { get; }

        //public String Group_resource_type { get; }

        //public GroupResource Group_resource { get; }

        public NotificationViewModel(Notification notification)
        {
            this.notification = notification;
        }
    }
}
