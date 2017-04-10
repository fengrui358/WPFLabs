using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLabs.Base;

namespace WpfLabs.FlexDataGrid
{
    /// <summary>
    /// MaskPanelControl.xaml 的交互逻辑
    /// </summary>
    public partial class MaskPanelControl : UserControl
    {
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            "IsExpanded", typeof(bool), typeof(MaskPanelControl),
            new PropertyMetadata(default(bool), IsExpandedChangedCallback));

        private static void IsExpandedChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var flexDataGrid = (MaskPanelControl)dependencyObject;
        }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool) GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public MaskPanelControl()
        {
            InitializeComponent();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var panel = TreeHelper.FindAncestorNode<StackPanel>((Control) sender);
            foreach (FrameworkElement panelChild in panel.Children)
            {
                if (panelChild.Tag != null && panelChild.Tag.ToString() == "CollapsedBtn")
                {
                    panelChild.Visibility = Visibility.Visible;
                }
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var panel = TreeHelper.FindAncestorNode<StackPanel>((Control) sender);
            foreach (FrameworkElement panelChild in panel.Children)
            {
                if (panelChild.Tag != null && panelChild.Tag.ToString() == "CollapsedBtn")
                {
                    panelChild.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}