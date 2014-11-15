using GoodReads.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodReads.ViewModels
{
    public class ShelvesViewModel
    {
        UserShelf shelf;

        public String BookCount { get; set; }

        public String Description { get; set; }

        public String DisplayFields { get; set; }

        public String ExclusiveFlag { get; set; }

        public String Featured { get; set; }

        public String Id { get; set; }

        public String Name { get; set; }

        public String Order { get; set; }

        public String PerPage { get; set; }

        public String RecommendFor { get; set; }

        public String Sort { get; set; }

        public String Sticky { get; set; }

        public ShelvesViewModel(UserShelf shelf)
        {
            this.shelf = shelf;
        }
    }
}
