using MyShelf.API.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyShelf.Controls
{
    public sealed partial class AddToShelfControl : UserControl
    {
        public string BookId
        {
            get { return (string)GetValue(BookIdProperty); }
            set { SetValue(BookIdProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BookId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookIdProperty =
            DependencyProperty.Register("BookId", typeof(string), typeof(AddToShelfControl), new PropertyMetadata(string.Empty));

        public IEnumerable<string> Shelves
        {
            get { return (IEnumerable<string>)GetValue(ShelvesProperty); }
            set { SetValue(ShelvesProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Shelves.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShelvesProperty =
            DependencyProperty.Register("Shelves", typeof(IEnumerable<string>), typeof(AddToShelfControl), new PropertyMetadata(null));

        //public object SelectedShelf
        //{
        //    get { return (object)GetValue(SelectedShelfProperty); }
        //    set { SetValue(SelectedShelfProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for SelectedShelf.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SelectedShelfProperty =
        //    DependencyProperty.Register("SelectedShelf", typeof(object), typeof(AddToShelfControl), new PropertyMetadata(null));


        public AddToShelfControl()
        {
            GetShelves();

            this.InitializeComponent();
        }

        private async System.Threading.Tasks.Task GetShelves()
        {
            //Shelves = (await ShelfService.Instance.GetShelvesList()).Select(s => s.Name);
        }

        private async void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ShelfService.Instance.AddBookToShelf("to-read", BookId);
        }

        private async void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ShelfService.Instance.AddBookToShelf("to-read", BookId, true);

        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newShelf = e.AddedItems.FirstOrDefault() as string;
            var oldShelf = e.RemovedItems.FirstOrDefault() as string;

            if (!string.IsNullOrEmpty(oldShelf))
                await ShelfService.Instance.AddBookToShelf(oldShelf, BookId, true);

            if (!string.IsNullOrEmpty(newShelf))
                await ShelfService.Instance.AddBookToShelf(newShelf, BookId);

        }
    }
}
