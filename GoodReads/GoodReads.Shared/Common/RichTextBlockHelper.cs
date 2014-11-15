﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace GoodReads.Common
{
    public class RichTextBlockHelper : DependencyObject
    {
        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  
        //This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string),
            typeof(RichTextBlockHelper),
            new PropertyMetadata(String.Empty, OnTextChanged));

        private static void OnTextChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RichTextBlock;
            if (control != null)
            {
                control.Blocks.Clear();
                string value = e.NewValue.ToString();

                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run { Text = value });
                control.Blocks.Add(paragraph);
            }
        }
    }
}
