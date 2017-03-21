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
using WpfLabs.ExpertPanel.Models;

namespace WpfLabs.ExpertPanel2
{
    /// <summary>
    /// ExpertPanel2.xaml 的交互逻辑
    /// </summary>
    public partial class ExpertPanel2 : UserControl
    {
        public static readonly DependencyProperty ItemSourcesProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable<ExpertModel>), typeof(ExpertPanel2),
            new PropertyMetadata(default(IEnumerable<ExpertModel>)));

        /// <summary>
        /// 专家数据源数据源
        /// </summary>
        public IEnumerable<ExpertModel> ItemsSource
        {
            get { return (IEnumerable<ExpertModel>) GetValue(ItemSourcesProperty); }
            set { SetValue(ItemSourcesProperty, value); }
        }

        public ExpertPanel2()
        {
            InitializeComponent();
        }

        private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}