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
using WpfLabs.PeopleResourcePanel.Models;

namespace WpfLabs.PeopleResourcePanel
{
    /// <summary>
    /// PeopleResourcePanel.xaml 的交互逻辑
    /// </summary>
    public partial class PeopleResourcePanel : UserControl
    {
        public static readonly DependencyProperty ItemSourcesProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable<PeopleModel>), typeof(PeopleResourcePanel),
            new PropertyMetadata(default(IEnumerable<PeopleModel>)));

        /// <summary>
        /// 专家数据源数据源
        /// </summary>
        public IEnumerable<PeopleModel> ItemsSource
        {
            get { return (IEnumerable<PeopleModel>) GetValue(ItemSourcesProperty); }
            set { SetValue(ItemSourcesProperty, value); }
        }

        public static readonly RoutedEvent CallPhoneEvent =
            EventManager.RegisterRoutedEvent("CallPhone", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PeopleModel>), typeof(PeopleResourcePanel));

        /// <summary>
        /// 触发打电话
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PeopleModel> CallPhone
        {
            add { AddHandler(CallPhoneEvent, value); }
            remove { RemoveHandler(CallPhoneEvent, value); }
        }

        public PeopleResourcePanel()
        {
            InitializeComponent();
        }

        private void Call_OnClick(object sender, RoutedEventArgs e)
        {
            var expert = ((FrameworkElement)sender).DataContext as PeopleModel;
            if (expert != null)
            {
                var args = new RoutedPropertyEventArgs<PeopleModel>(expert);
                args.RoutedEvent = CallPhoneEvent;

                RaiseEvent(args);
            }
        }
    }
}