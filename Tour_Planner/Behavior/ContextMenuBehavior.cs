using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace Tour_Planner.Behavior {
    public class ContextMenuBehavior : Behavior<ContextMenu> {
        public static ContextMenu GetContextMenu(DependencyObject obj) {
            return (ContextMenu)obj.GetValue(ContextMenuProperty);
        }

        public static void SetContextMenu(DependencyObject obj, ContextMenu value) {
            obj.SetValue(ContextMenuProperty, value);
        }

        public static readonly DependencyProperty ContextMenuProperty =
            DependencyProperty.RegisterAttached("ContextMenu", typeof(ContextMenu), typeof(ContextMenuBehavior), new PropertyMetadata(null, OnContextMenuChanged));

        private static void OnContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is FrameworkElement frameworkElement) {
                frameworkElement.Loaded += (sender, args) => {
                    var item = sender as ListBoxItem;
                    if (item != null) {
                        var contextMenu = GetContextMenu(item);
                        if (contextMenu != null) {
                            contextMenu.DataContext = item.DataContext;
                            item.ContextMenu = contextMenu;
                        }
                    }
                };
            }
        }
    }
}
