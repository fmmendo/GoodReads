using MyShelf.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



namespace MyShelf.Controls
{
    public sealed partial class MainFrame : UserControl
    {
        MainFrameViewModel ViewModel => MainFrameViewModel.Instance;
        public bool ShowAds => !API.Storage.MyShelfSettings.Instance.DontShowAds;

        TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public Task EnsureLoadedAsync { get; }

        public MainFrame()
        {
            InitializeComponent();

            EnsureLoadedAsync = _tcs.Task;
        }

        private async void RootFrame_Loaded(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Bounds.Width >= 1280)
                VisualStateManager.GoToState(this, "WideState", false);

            ViewModel.RootFrame = RootFrame;

            if (!EnsureLoadedAsync.IsCompleted)
            {
                _tcs.TrySetResult(true);
            }
        }
    }
}
