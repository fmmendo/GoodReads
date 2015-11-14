using Mendo.UAP.Common;
using MyShelf.API.Services;
using MyShelf.Pages;
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
        
        public object SelectedWork { get; set; }

        public bool IsLoading { get { return Get(false); } set { Set(value); } }

        public async Task SearchClick()
        {
            IsLoading = true;
            var results = await BookService.Instance.Search(SearchTerm);

            Results.Clear();
            foreach (var work in results.Results.Work)
            {
                if (!String.IsNullOrEmpty(work?.BestBook?.Id))
                    Results.Add(new WorkViewModel(work));
            }
            IsLoading = false;
        }



        public void SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var work = e.AddedItems.First() as WorkViewModel;

                NavigationService.Navigate(typeof(BookPage), work.BestBookId);
        }
    }
}
