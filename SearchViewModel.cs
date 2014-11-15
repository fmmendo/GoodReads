using GoodReads.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodReads
{
    public class SearchViewModel : BindableBase
    {
        private ObservableCollection<String> _titles = new ObservableCollection<string>();
        public ObservableCollection<String> Titles
        {
            get { return this._titles; }
            set { this.SetProperty(ref this._titles, value); }
        }
    }
}
  