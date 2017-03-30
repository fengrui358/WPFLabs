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
using WpfLabs.FlexDataGrid.Models;

namespace WpfLabs.FlexDataGrid
{
    /// <summary>
    /// FlexDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class FlexDataGrid : UserControl
    {
        public static readonly DependencyProperty ItemSourcesProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable<FlexDataGridItemModel>), typeof(FlexDataGrid),
            new PropertyMetadata(default(IEnumerable<FlexDataGridItemModel>)));

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<FlexDataGridItemModel> ItemsSource
        {
            get { return (IEnumerable<FlexDataGridItemModel>) GetValue(ItemSourcesProperty); }
            set { SetValue(ItemSourcesProperty, value); }
        }

        public FlexDataGrid()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //找到自身的承载面板
            var panel = TreeHelper.FindAncestorNode<Panel>((Control) sender);
            if (panel.Tag.ToString() == "ShrinkStatus")
            {
                panel.Visibility = Visibility.Hidden;
            }

            //找到DataGrid行中的隐藏遮罩层容器
            var dataGridRow = TreeHelper.FindAncestorNode<DataGridRow>((Control) sender);
            var gridContainer = TreeHelper.GetVisualChild<Grid>(dataGridRow, "DataGridRowMask");

            var maskPanelControl = new MaskPanelControl {HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(1)};
            gridContainer.Children.Add(maskPanelControl);

            maskPanelControl.IsExpanded = true;
        }

        private void CreateMaskControl()
        {
            var stackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 266,
                Opacity = 0.5
            };
        }
    }
}