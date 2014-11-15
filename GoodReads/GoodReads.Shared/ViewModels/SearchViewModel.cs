using GoodReads.API;
using GoodReads.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace GoodReads.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private GoodReadsAPI _gr = App.Goodreads;

        private string searchTerm = string.Empty;

        private ObservableCollection<WorkViewModel> results = new ObservableCollection<WorkViewModel>();

        public ObservableCollection<WorkViewModel> Results
        {
            get { return results; }
            set { results = value; NotifyPropertyChanged(); }
        }


        public SearchViewModel()
        {
            Results.CollectionChanged += Works_CollectionChanged;

            BookClickCommand = new RelayCommand<ItemClickEventArgs>(UserClick);
        }

        private void Works_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("Results");
        }

        public string SearchTerm
        {
            get { return searchTerm; }
            set { searchTerm = value; NotifyPropertyChanged(); }
        }

        public async void Search(string search)
        {
            var results = await _gr.Search(search);

            if (results == null)
                return;
            
            Results = new ObservableCollection<WorkViewModel>();
            foreach (var work in results.Results.Work)
            {
                Results.Add(new WorkViewModel(work));
            }
        }


        #region Commands
        /// <summary>
        /// Command called when a user clicks a book in the MY BOOKS section
        /// </summary>
        public RelayCommand<ItemClickEventArgs> BookClickCommand { get; set; }

        /// <summary>
        /// Handles the click event on a book
        /// </summary>
        /// <param name="args"></param>
        public void UserClick(ItemClickEventArgs args)
        {
            var book = args.ClickedItem as WorkViewModel;
            if (book == null) return;

            App.NavigationService.Navigate(typeof(BookPage), book.Id);
        }
        #endregion
    }
}
