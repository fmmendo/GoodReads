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

        private async void RootFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Bounds.Width >= 1280)
                VisualStateManager.GoToState(this, "WideState", false);

            if (!EnsureLoadedAsync.IsCompleted)
            {
                _frameTcs.TrySetResult(true);
            }
        }
    }
}
