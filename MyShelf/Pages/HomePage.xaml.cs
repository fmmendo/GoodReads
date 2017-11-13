using Mendo.UWP.Common;
using Mendo.UWP.Extensions;
using MyShelf.ViewModels;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyShelf.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : PageBase
    {
        HomePageViewModel ViewModel => HomePageViewModel.Instance;

        public HomePage()
        {
            InitializeComponent();

            ConfigureComposition();
        }

        private void ConfigureComposition()
        {
            Logo.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));
            this.Search.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));
        }

        private async void Instance_AuthStateChanged(object sender, API.Services.AuthState e)
        {
            if (IsCurrentPage)
                ViewModel.Refresh();
        }

        protected override async void LoadState(object parameter, Dictionary<string, object> pageState)
        {
            base.LoadState(parameter, pageState);

            API.Services.AuthenticationService.Instance.AuthStateChanged += Instance_AuthStateChanged;

            ViewModel.Refresh();
        }

        protected override void SaveState(NavigationEventArgs e, Dictionary<string, object> pageState)
        {
            API.Web.ApiClient.Instance.ResetQueue();
            API.Services.AuthenticationService.Instance.AuthStateChanged -= Instance_AuthStateChanged;

            base.SaveState(e, pageState);
        }

        private void lvUpdates_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.ItemContainer.Loaded += ItemContainer_Loaded;
        }

        private void ItemContainer_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsPanel = (ItemsStackPanel)this.lvUpdates.ItemsPanelRoot;
            var itemContainer = (ListViewItem)sender;

            var itemIndex = this.lvUpdates.IndexFromContainer(itemContainer);

            var relativeIndex = itemIndex - itemsPanel.FirstVisibleIndex;

            var uc = itemContainer.ContentTemplateRoot as Grid;

            if (/*itemIndex != _persistedItemIndex && */itemIndex >= 0 && itemIndex >= itemsPanel.FirstVisibleIndex && itemIndex <= itemsPanel.LastVisibleIndex)
            {
                var itemVisual = ElementCompositionPreview.GetElementVisual(uc);
                ElementCompositionPreview.SetIsTranslationEnabled(uc, true);

                var easingFunction = Window.Current.Compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.2f, 1f));

                // Create KeyFrameAnimations
                var offsetAnimation = Window.Current.Compositor.CreateScalarKeyFrameAnimation();
                offsetAnimation.InsertKeyFrame(0f, 100);
                offsetAnimation.InsertKeyFrame(1f, 0, easingFunction);
                offsetAnimation.Target = "Translation.Y";
                offsetAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
                offsetAnimation.Duration = TimeSpan.FromMilliseconds(700);
                offsetAnimation.DelayTime = TimeSpan.FromMilliseconds(relativeIndex * 100);

                var fadeAnimation = Window.Current.Compositor.CreateScalarKeyFrameAnimation();
                fadeAnimation.InsertExpressionKeyFrame(0f, "0");
                fadeAnimation.InsertExpressionKeyFrame(1f, "1");
                fadeAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
                fadeAnimation.Duration = TimeSpan.FromMilliseconds(700);
                fadeAnimation.DelayTime = TimeSpan.FromMilliseconds(relativeIndex * 100);

                // Start animations
                itemVisual.StartAnimation("Translation.Y", offsetAnimation);
                itemVisual.StartAnimation("Opacity", fadeAnimation);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Skipping");
            }

            itemContainer.Loaded -= this.ItemContainer_Loaded;
        }

        private void Search_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Search", Search);


            NavigationService.Navigate(typeof(Pages.SearchPage), args.QueryText);
        }
    }
}
