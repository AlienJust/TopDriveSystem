using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

//using Microsoft.Xaml.Behaviors;

namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
    public class AutoScrollingBehavior : Behavior<ScrollViewer>
    {
        private const double Tolerance = 1.0;

        public static readonly DependencyProperty UpdateTriggerProperty =
            DependencyProperty.Register("UpdateTrigger", typeof(object), typeof(AutoScrollingBehavior),
                new UIPropertyMetadata(Update));

        public static readonly DependencyProperty IsScrolledDownProperty =
            DependencyProperty.Register("IsScrolledDown", typeof(bool), typeof(AutoScrollingBehavior),
                new UIPropertyMetadata(false));

        public object UpdateTrigger
        {
            get => GetValue(UpdateTriggerProperty);
            set => SetValue(UpdateTriggerProperty, value);
        }

        private bool IsScrolledDown
        {
            get => (bool) GetValue(IsScrolledDownProperty);
            set => SetValue(IsScrolledDownProperty, value);
        }

        private static void Update(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) d.GetValue(IsScrolledDownProperty))
            {
                var scroll = ((AutoScrollingBehavior) d).AssociatedObject;
                scroll.ScrollToBottom();
            }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
            AssociatedObject.IsVisibleChanged += AssociatedObjectOnIsVisibleChanged;
        }

        private void AssociatedObjectOnIsVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            IsScrolledDown = CalculateIsScrollDown();
        }

        private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var isScrollDown = CalculateIsScrollDown();
            IsScrolledDown = isScrollDown;
            if (isScrollDown) AssociatedObject.ScrollToEnd();
        }

        private bool CalculateIsScrollDown()
        {
            return Math.Abs(AssociatedObject.VerticalOffset - AssociatedObject.ScrollableHeight) < Tolerance;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            IsScrolledDown = CalculateIsScrollDown();
        }
    }
}