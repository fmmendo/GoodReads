using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mendo.UWP.Common
{
    public class BasePage : Page
    {
        public BasePage()
        {
            //// When this page is part of the visual tree make two changes:
            //// 1) Map application view state to visual state for the page
            //// 2) Handle hardware navigation requests
            //Loaded += (sender, e) =>
            //{
            //    //#if WINDOWS_PHONE_APP
            //    //                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //    //#else
            //    //                StartLayoutUpdates(sender, e);
            //    // Keyboard and mouse navigation only apply when occupying the entire window
            //    if (ActualHeight == Window.Current.Bounds.Height && ActualWidth == Window.Current.Bounds.Width)
            //    {
            //        // Listen to the window directly so focus isn't required
            //        Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += CoreDispatcher_AcceleratorKeyActivated;
            //        Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
            //    }
            //    //#endif
            //};

            //// Undo the same changes when the page is no longer visible
            //Unloaded += (sender, e) =>
            //{
            //    //#if WINDOWS_PHONE_APP
            //    //                Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            //    //#else
            //    //                StopLayoutUpdates(sender, e);
            //    Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -= CoreDispatcher_AcceleratorKeyActivated;
            //    Window.Current.CoreWindow.PointerPressed -= CoreWindow_PointerPressed;
            //    //#endif
            //};
        }

        #region Navigation

        /// <summary>
        /// Returns true if this page is the current displayed content of it's parent frame
        /// </summary>
        public Boolean IsCurrentPage => Frame.Content == this;

        /// <summary>
        /// Determines if we can go back in the stack
        /// </summary>
        /// <returns>
        /// True if there's at least one entry in the back navigation history.
        /// </returns>
        public virtual bool CanGoBack() => Frame != null && Frame.CanGoBack;

        /// <summary>
        /// Determines if we can go forward in the stack
        /// </summary>
        /// <returns>
        /// True if there's at least one entry in the forward navigation history.
        /// </returns>
        public virtual bool CanGoForward() => Frame != null && Frame.CanGoForward;

        /// <summary>
        /// Navigates back
        /// </summary>
        public virtual void GoBack()
        {
            if (Frame != null && Frame.CanGoBack)
                Frame.GoBack();
        }
        /// <summary>
        /// Navigates forward
        /// </summary>
        public virtual void GoForward()
        {
            if (Frame != null && Frame.CanGoForward)
                Frame.GoForward();
        }

        /// <summary>
        /// Invoked on every keystroke, including system keys such as Alt key combinations, when
        /// this page is active and occupies the entire window.  Used to detect keyboard navigation
        /// between pages even when the page itself doesn't have focus.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            var virtualKey = e.VirtualKey;

            // Only investigate further when Left, Right, or the dedicated Previous or Next keys
            // are pressed
            if ((e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown || e.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
                (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right || (int)virtualKey == 166 || (int)virtualKey == 167))
            {
                var coreWindow = Window.Current.CoreWindow;
                var downState = CoreVirtualKeyStates.Down;
                bool menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
                bool controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
                bool shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
                bool noModifiers = !menuKey && !controlKey && !shiftKey;
                bool onlyAlt = menuKey && !controlKey && !shiftKey;

                if (((int)virtualKey == 166 && noModifiers) || (virtualKey == VirtualKey.Left && onlyAlt))
                {
                    // When the previous key or Alt+Left are pressed navigate back
                    e.Handled = true;
                    GoBack();
                }
                else if (((int)virtualKey == 167 && noModifiers) || (virtualKey == VirtualKey.Right && onlyAlt))
                {
                    // When the next key or Alt+Right are pressed navigate forward
                    e.Handled = true;
                    GoForward();
                }
            }
        }

        /// <summary>
        /// Invoked on every mouse click, touch screen tap, or equivalent interaction when this
        /// page is active and occupies the entire window.  Used to detect browser-style next and
        /// previous mouse button clicks to navigate between pages.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="e">Event data describing the conditions that led to the event.</param>
        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            var properties = e.CurrentPoint.Properties;

            // Ignore button chords with the left, right, and middle buttons
            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
                properties.IsMiddleButtonPressed)
                return;

            // If back or forward are pressed (but not both) navigate appropriately
            bool backPressed = properties.IsXButton1Pressed;
            bool forwardPressed = properties.IsXButton2Pressed;
            if (backPressed ^ forwardPressed)
            {
                e.Handled = true;
                if (backPressed)
                    GoBack();

                if (forwardPressed)
                    GoForward();
            }
        }

        #endregion

        #region Lifetime Management

        private string _pageKey;

        public string InternalPageKey => _pageKey;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the group to be displayed.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Returning to a cached page through navigation shouldn't trigger state loading
            if (_pageKey != null)
                return;

            var frameState = SuspensionManager.SessionStateForFrame(Frame);
            _pageKey = e.Parameter.ToString();

            if (e.NavigationMode == NavigationMode.New)
            {
                // Clear existing state for forward navigation when adding a new page to the
                // navigation stack
                var nextPageKey = _pageKey;
                int nextPageIndex = Frame.BackStackDepth;
                while (frameState.Remove(nextPageKey))
                {
                    nextPageIndex++;
                    nextPageKey = "Page-" + nextPageIndex;
                }

                var parameter = SuspensionManager.ParameterStates[_pageKey];
                // Pass the navigation parameter to the new page
                LoadState(parameter, null);
            }
            else
            {
                // Pass the navigation parameter and preserved page state to the page, using
                // the same strategy for loading suspended state and recreating pages discarded
                // from cache
                //LoadState(e, (Dictionary<String, Object>)frameState[_pageKey]);
                var parameter = SuspensionManager.ParameterStates[_pageKey];
                LoadState(parameter, (Dictionary<String, Object>)frameState[_pageKey]);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        /// <summary>
        /// Invoked when this page will no longer be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the group to be displayed.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {
                var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
                var pageState = new Dictionary<string, object>();
                this.SaveState(e, pageState);
                frameState[_pageKey] = pageState;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected virtual void LoadState(object parameter, Dictionary<string, object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected virtual void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
        }

        #endregion

        /// <summary>
        /// Optional container for a command bar on desktop/tablet view
        /// </summary>
        public FrameworkElement TopBarContent
        {
            get { return (FrameworkElement)GetValue(TopBarContentProperty); }
            set { SetValue(TopBarContentProperty, value); }
        }

        public static readonly DependencyProperty TopBarContentProperty =
            DependencyProperty.Register("TopBarContent", typeof(FrameworkElement), typeof(BasePage), new PropertyMetadata(null));
    }
}
