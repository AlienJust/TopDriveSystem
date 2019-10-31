//using Microsoft.Xaml.Behaviors;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace TopDriveSystem.ConfigApp.BsEthernetLogs
{
    public class AppendTextBehavior : Behavior<TextBox>
    {
        // Using a DependencyProperty as the backing store for AppendTextAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppendTextActionProperty =
            DependencyProperty.Register("AppendTextAction", typeof(Action<string>), typeof(AppendTextBehavior),
                new PropertyMetadata(null));

        public Action<string> AppendTextAction
        {
            get => (Action<string>) GetValue(AppendTextActionProperty);
            set => SetValue(AppendTextActionProperty, value);
        }

        protected override void OnAttached()
        {
            SetCurrentValue(AppendTextActionProperty, (Action<string>) AssociatedObject.AppendText);
            base.OnAttached();
        }
    }
}