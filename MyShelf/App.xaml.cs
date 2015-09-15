using Mendo.UAP.Common;
using MyShelf.Controls;
using MyShelf.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.ExtendedExecution;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MyShelf
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private readonly string AppLaunchCount = "APP_LAUNCH_COUNT_SETTING";

        public MainFrame Root = null;

        public Frame RootFrame { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync();
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            CreateRootFrame(e.PreviousExecutionState, e.Arguments);

            RateMyApp();

            try
            {
                // Install the main VCD. Since there's no simple way to test that the VCD has been imported, 
                // or that it's your most recent version, it's not unreasonable to do this upon app load.
                StorageFile vcdStorageFile = await Package.Current.InstalledLocation.GetFileAsync(@"CortanaCommands.xml");
                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            switch (args.Kind)
            {
                case ActivationKind.VoiceCommand:
                    //VoiceService.Instance.ProcessVoiceCommand(args);
                    break;
                case ActivationKind.ToastNotification:
                    break;
                case ActivationKind.Search:
                    break;
            }

            await CreateRootFrame(args.PreviousExecutionState, null);
        }

        private async Task CreateRootFrame(ApplicationExecutionState executionState, string args)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Root = Window.Current.Content as MainFrame;
            //Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (RootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                Root = new MainFrame();

                // Place the frame in the current Window
                Window.Current.Content = Root;

                await Root.EnsureLoadedAsync;

                RootFrame = Root.RootFrame;
                RootFrame.CacheSize = 6;
                RootFrame.NavigationFailed += OnNavigationFailed;
                NavigationService.SetFrame(RootFrame);

                // Register this frame with the suspension manager
                SuspensionManager.RegisterFrame(RootFrame, "appFrame");

                if (executionState == ApplicationExecutionState.Terminated)
                {
                    // Attempt to restore the navigation state if the application was terminated
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                        await SuspensionManager.DeleteSavedStatesAsync();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                //Window.Current.Content = rootFrame;
            }

            if (RootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                NavigationService.Navigate(typeof(HomePage), args);
                //RootFrame.Navigate(typeof(HomePage), args);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            //ExtendedExecutionSession session = new ExtendedExecutionSession();
            //session.Description = "Persisting data and maintaining cache";
            //session.Reason = ExtendedExecutionReason.SavingData;

            //var result = await session.RequestExtensionAsync();

            await SuspensionManager.SaveAsync(); // Persists the per-page view models to file
            //session.PercentProgress = 50;

            //await FileCache.GzipCache.TrimAsync();
            //session.PercentProgress = 100;

            deferral.Complete();
        }


        #region Rate My App
        private void RateMyApp()
        {
            var launchcount = Settings.Get(AppLaunchCount, SettingsLocation.Local);
            if (launchcount == null)
                Settings.Set(AppLaunchCount, 1, SettingsLocation.Local);
            else if ((int)launchcount == 2)
            {
                MessageDialog md = new MessageDialog("Thank you for using myShelf, would you like to rate the app?", "Rate myShelf?");
                md.Commands.Add(new UICommand(
                    "No",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                md.Commands.Add(new UICommand(
                    "Yes",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));

                // Set the command that will be invoked by default
                md.DefaultCommandIndex = 1;

                // Set the command to be invoked when escape is pressed
                md.CancelCommandIndex = 0;
                md.ShowAsync();

                Settings.Set(AppLaunchCount, ((int)launchcount) + 1, SettingsLocation.Local, true);
            }
            else if ((int)launchcount > 2)
                return;
            else
                Settings.Set(AppLaunchCount, ((int)launchcount) + 1, SettingsLocation.Local, true);
        }

        private async void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=9WZDNCRDR8RZ"));
            }
        }
        #endregion
    }
}
