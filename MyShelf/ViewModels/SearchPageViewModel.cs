using Mendo.UAP.Common;
using MyShelf.API.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public string SearchTerm { get; set; }

        public ObservableCollection<WorkViewModel> Results { get; set; } = new ObservableCollection<WorkViewModel>();

        public async Task SearchClick()
        {
            var results = await BookService.Instance.Search(SearchTerm);

            foreach (var work in results.Results.Work)
            {
                Results.Add(new WorkViewModel(work));
            }
        }
    }
}
