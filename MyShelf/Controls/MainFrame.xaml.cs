using MyShelf.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



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
