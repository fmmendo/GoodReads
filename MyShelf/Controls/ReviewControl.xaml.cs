using MyShelf.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
    public sealed partial class ReviewControl : UserControl
    {
        public ReviewControl()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, HiddenState.Name, false);
        }

        public UserStatusViewModel Review
        {
            get { return (UserStatusViewModel)GetValue(ReviewProperty); }
            set { SetValue(ReviewProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Review.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReviewProperty =
            DependencyProperty.Register("Review", typeof(UserStatusViewModel), typeof(ReviewControl), new PropertyMetadata(null));

        public string ReviewId { get; set; }
        public bool IsPosting { get; set; }
        public double Rating { get; set; }
        public string ReviewBody { get; set; }
        public DateTime ReadAt { get; set; } = DateTime.Today;

        private void rect_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!IsPosting)
                VisualStateManager.GoToState(this, HiddenState.Name, true);
        }

        public async void Show()
        {
            if (Review == null)
                return;

                var r = await API.Services.BookService.Instance.GetUserReview(Review.BookId);
                ReviewId = r.Id;

            //Review = new PostRatingsRequest();
            //Review.OrderId = OrderId;

            if (DisplayStates.CurrentState == HiddenState)
                VisualStateManager.GoToState(this, VisibleState.Name, true);
        }

        public void Hide()
        {
            if (!IsPosting)
                VisualStateManager.GoToState(this, HiddenState.Name, true);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            IsPosting = true;
            await API.Services.BookService.Instance.EditReview(ReviewId, "true", ReviewBody, Rating.ToString(), ReadAt.ToString("yyyy-MM-dd"), "read");
            IsPosting = false;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
