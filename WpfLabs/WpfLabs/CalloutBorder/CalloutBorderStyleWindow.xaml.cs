using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLabs.CalloutBorder
{
    /// <summary>
    /// CalloutBorderStyleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalloutBorderStyleWindow : Window, INotifyPropertyChanged
    {
        private CalloutBorder.CalloutPlacement _placement;

        public CalloutBorder.CalloutPlacement Placement
        {
            get => _placement;
            set
            {
                _placement = value;
                OnPropertyChanged();
            }
        }

        public CalloutBorderStyleWindow()
        {
            InitializeComponent();
        }

        private void PlacementRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            Placement = (CalloutBorder.CalloutPlacement) Enum.Parse(typeof(CalloutBorder.CalloutPlacement),
                ((RadioButton) sender).Content.ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
