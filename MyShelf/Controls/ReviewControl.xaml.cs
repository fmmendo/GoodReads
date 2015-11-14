using Mendo.UAP.Common;
using MyShelf.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class ReviewControl : UserControl
    {
        public UserStatusViewModel Review
        {
            get { return (UserStatusViewModel)GetValue(ReviewProperty); }
            set { SetValue(ReviewProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Review.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReviewProperty = DependencyProperty.Register("Review", typeof(UserStatusViewModel), typeof(ReviewControl), new PropertyMetadata(null));

        public bool IsPosting
        {
            get { return (bool)GetValue(IsPostingProperty); }
            set { SetValue(IsPostingProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IsPosting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPostingProperty =  DependencyProperty.Register("IsPosting", typeof(bool), typeof(ReviewControl), new PropertyMetadata(false));



        public string ReviewId { get; set; }
        public double Rating { get; set; }
        public string ReviewBody { get; set; } = string.Empty;
        public DateTime ReadAt { get; set; } = DateTime.Today;

        public ReviewControl()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, HiddenState.Name, false);

            if (DeviceInformation.HasPhoneHardwareButtons)
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed; ;
            }
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (DisplayStates.CurrentState == VisibleState)
                Hide();
        }

        private void rect_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!IsPosting)
                VisualStateManager.GoToState(this, HiddenState.Name, true);
        }

        public async void Show()
        {
            if (Review == null)
                return;

            if (DisplayStates.CurrentState == HiddenState)
                VisualStateManager.GoToState(this, VisibleState.Name, true);

            var r = await API.Services.BookService.Instance.GetUserReview(Review.BookId);
            ReviewId = r.Id;
        }

        public void Hide()
        {
            if (!IsPosting)
                VisualStateManager.GoToState(this, HiddenState.Name, true);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            IsPosting = true;
            var success = await API.Services.BookService.Instance.EditReview(ReviewId, "true", ReviewBody, Rating.ToString(), ReadAt.ToString("yyyy-MM-dd"), "read");
            IsPosting = false;

            if (success)
                Hide();
            else
            {
                var md = new MessageDialog("Couldn't post review. Please try again later");
                md.ShowAsync();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
