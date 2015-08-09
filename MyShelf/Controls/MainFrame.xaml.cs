using MyShelf.ViewModels;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class MainFrame : UserControl
    {
        MainFrameViewModel ViewModel => MainFrameViewModel.Instance;

        TaskCompletionSource<bool> _frameTcs = new TaskCompletionSource<bool>();

        public Task EnsureLoadedAsync { get; }

        public MainFrame()
        {
            InitializeComponent();

            EnsureLoadedAsync = _frameTcs.Task;
        }

        private void RootFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EnsureLoadedAsync.IsCompleted)
            {
                _frameTcs.TrySetResult(true);
            }
        }
    }
}
