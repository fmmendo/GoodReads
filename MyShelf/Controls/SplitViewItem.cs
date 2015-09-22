using Mendo.UAP.Extensions;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace MyShelf.Controls
{
    public sealed class SplitViewItem : RadioButton
    {
        public SplitViewItem()
        {
            DefaultStyleKey = typeof(SplitViewItem);
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(String), typeof(SplitViewItem), new PropertyMetadata("A"));

        //public Geometry PathData
        //{
        //    get { return (Geometry)GetValue(PathDataProperty); }
        //    set { SetValue(PathDataProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for PathData.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PathDataProperty =
        //    DependencyProperty.Register("PathData", typeof(Geometry), typeof(SplitViewItem), new PropertyMetadata(null));

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(SplitViewItem), new PropertyMetadata("SplitViewButton"));

        //public bool UsePath
        //{
        //    get { return (bool)GetValue(UsePathProperty); }
        //    set { SetValue(UsePathProperty, value); }
        //}
        //// Using a DependencyProperty as the backing store for UsePath.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty UsePathProperty =
        //    DependencyProperty.Register("UsePath", typeof(bool), typeof(SplitViewItem), new PropertyMetadata(false));

        /// <summary>
        /// If true, automatically closes the parent SplitView
        /// </summary>
        public bool ClosesParent
        {
            get { return (bool)GetValue(ClosesParentProperty); }
            set { SetValue(ClosesParentProperty, value); }
        }
        public static readonly DependencyProperty ClosesParentProperty =
            DependencyProperty.Register("ClosesParent", typeof(bool), typeof(SplitViewItem), new PropertyMetadata(true));

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            if (ClosesParent)
            {
                SplitView sp = this.GetParentByType<SplitView>();
                if (sp != null)
                    sp.IsPaneOpen = false;
            }

            base.OnTapped(e);
        }
    }
}
