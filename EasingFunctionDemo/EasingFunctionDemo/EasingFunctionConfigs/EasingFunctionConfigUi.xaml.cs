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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    /// <summary>
    /// EasingFunctionConfigUi.xaml 的交互逻辑
    /// </summary>
    public partial class EasingFunctionConfigUi : UserControl
    {
        public EasingFunctionBase EasingFunctionBase { get; private set; }

        public EasingFunctionConfigUi(EasingFunctionBase easingFunction)
        {
            EasingFunctionBase = easingFunction;
            InitializeComponent();

            DataContext = EasingFunctionBase;
        }

        /// <summary>
        /// 反射以增加Ui配置项
        /// </summary>
        private void CreateUi()
        {
            
        }
    }
}
