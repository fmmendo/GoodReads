using Mendo.UWP.Common;
using Mendo.UWP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationRoot : Page
    {
        private static NavigationRoot _instance;
        private bool hasLoadedPreviously;

        public NavigationRoot()
        {
            _instance = this;
            this.InitializeComponent();

            //windowTitle.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));

            //var nav = SystemNavigationManager.GetForCurrentView();

            //nav.BackRequested += Nav_BackRequested;
        }

        public static NavigationRoot Instance => _instance;
        public Frame AppFrame => appNavFrame;
        public TitleBarHelper TitleHelper => TitleBarHelper.Instance;

        public void ToggleFullscreen()
        {
            ViewModeService.Instance.ToggleFullscreen();
        }

        public void ExitFullScreen()
        {
            ViewModeService.Instance.DoExitFullscreen();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //if (e.Parameter is Episode)
            //{
            //    AppFrame.Navigate(typeof(Player), e.Parameter);
            //}
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModeService.Instance.UnRegister();
        }

        private void Nav_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //var ignored = _navigationService.GoBackAsync();
            e.Handled = true;
        }
    }
}
