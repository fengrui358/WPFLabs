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

namespace WpfLabs.NotifyMsgPresenter
{
    /// <summary>
    /// NotifyMsgItem.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyMsgItem : UserControl
    {
        public NotifyMsgModel NotifyMsgModel { get; set; }

        public NotifyMsgItem(NotifyMsgModel notifyMsgModel)
        {
            NotifyMsgModel = notifyMsgModel;
            InitializeComponent();
        }
    }
}
