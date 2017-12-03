using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    /// <summary>
    /// EasingFunctionConfigUi.xaml 的交互逻辑
    /// </summary>
    public partial class EasingFunctionConfigUi
    {
        public EasingFunctionBase EasingFunctionBase { get; }

        public event EventHandler ConfigEasingFunctionChanged;

        public EasingFunctionConfigUi(EasingFunctionBase easingFunction)
        {
            EasingFunctionBase = easingFunction;
            InitializeComponent();

            DataContext = EasingFunctionBase;
            CreateUi();
        }

        /// <summary>
        /// 反射以增加Ui配置项
        /// </summary>
        private void CreateUi()
        {
            if (Container.Children.Count > 1)
            {
                Container.Children.RemoveRange(1, Container.Children.Count);
            }

            var properties = EasingFunctionBase.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var propertyInfo in properties)
            {
                var grid = new Grid {Margin = new Thickness(5, 0, 5, 0)};
                grid.ColumnDefinitions.Add(new ColumnDefinition {MinWidth = 100, Width = GridLength.Auto});
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                var label = new Label {Content = propertyInfo.Name, HorizontalAlignment = HorizontalAlignment.Right};
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                var tb = new TextBox {VerticalAlignment = VerticalAlignment.Center, MinWidth = 100};
                var binding = new Binding(propertyInfo.Name)
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                tb.SetBinding(TextBox.TextProperty, binding);
                Grid.SetColumn(tb, 1);
                grid.Children.Add(tb);

                tb.TextChanged += (sender, args) =>
                {
                    ConfigEasingFunctionChanged?.Invoke(this, EventArgs.Empty);
                };

                Container.Children.Add(grid);
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConfigEasingFunctionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}