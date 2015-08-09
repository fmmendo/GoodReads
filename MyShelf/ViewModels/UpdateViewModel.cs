using Mendo.UAP.Common;
using MyShelf.API.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class UpdateViewModel : ViewModelBase
    {
        public string ActionText { get; set; }
        public string UserName { get; set; }
        //public string Link { get; set; }
        public string ImageUrl { get; set; }
        //public User Actor { get; set; }
        //public string UpdatedAt { get; set; }
        //public UpdateObject Object { get; set; }
        //public string Type { get; set; }
        //public UpdateAction Action { get; set; }
        //public string Body { get; set; }

        public UpdateViewModel(Update update)
        {
            if (update == null)
                return;

            ImageUrl = update.Actor?.ImageUrl;
            UserName = update.Actor?.Name;
            ActionText = update.ActionText.Contains("<") ? Regex.Replace(update.ActionText, "<.*?>", string.Empty) : update.ActionText;
        }
    }
}
