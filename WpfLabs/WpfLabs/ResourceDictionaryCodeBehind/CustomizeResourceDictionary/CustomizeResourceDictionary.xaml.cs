using System.Windows;
using System.Windows.Input;

namespace WpfLabs.ResourceDictionaryCodeBehind.CustomizeResourceDictionary
{
    public partial class CustomizeResourceDictionary
    {
        public CustomizeResourceDictionary() 
        {
            InitializeComponent();
        }

        private void TextBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Window) ((FrameworkElement) sender).TemplatedParent).DragMove();
        }

        private void WindowClose_OnClick(object sender, RoutedEventArgs e)
        {
            ((Window)((FrameworkElement)sender).TemplatedParent).Close();
        }
    }
}
