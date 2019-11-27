using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using FrHello.NetLib.Core.Wpf.UiConverters;

namespace WpfLabs.ItemsControlWithTimeline
{
    /// <summary>
    /// Interaction logic for ItemsControlWithTimelineWindow.xaml
    /// </summary>
    public partial class ItemsControlWithTimelineWindow : Window
    {
        public List<Item> Items { get; set; }

        public ItemsControlWithTimelineWindow()
        {
            InitializeComponent();

            DataContext = this;

            var items = new List<Item>();
            for (int i = 0; i < 3; i++)
            {
                var x = Convert(false, i, typeof(bool), null, CultureInfo.CurrentCulture);
                items.Add(new Item
                {
                    Content = Guid.NewGuid().ToString(),
                    CreateTime = DateTime.Now
                });
            }

            Items = items;
        }

        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="converterValue">将值处理为bool值，在根据真假和目标类型转换为可处理的值</param>
        /// <param name="value">需要处理的值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">比较参数</param>
        /// <param name="culture">文化</param>
        /// <returns></returns>
        internal static object Convert(bool converterValue, object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            //判断结果能否进行转换
            if (true)
            {
                if (parameter != null)
                {
                    //如果parameter是传的一个类型，则进行类型是否相等的比较
                    if (parameter is Type parameterType)
                    {
                        if (value != null && value.GetType() == parameterType)
                        {
                            converterValue = !converterValue;
                        }
                    }

                    //如果参数不等于空则是判断比较
                    if (value != null && value.Equals(parameter))
                    {
                        converterValue = !converterValue;
                    }
                }
                else
                {
                    if (value is bool boolValue)
                    {
                        //如果值为布尔值，则优先进行判断
                        if (boolValue)
                        {
                            converterValue = !converterValue;
                        }
                    }
                    else if (value is string str)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            converterValue = !converterValue;
                        }
                    }
                    else if (value is IEnumerable enumerableValue)
                    {
                        //如果值为集合则根据数量进行判断
                        var notEmpty = false;

                        foreach (var unused in enumerableValue)
                        {
                            notEmpty = true;
                            break;
                        }

                        if (notEmpty)
                        {
                            converterValue = !converterValue;
                        }
                    }
                    else
                    {
                        //如果参数等于空则进行一些基本判断
                        if (value != null && value != DependencyProperty.UnsetValue)
                        {
                            if (double.TryParse(value.ToString(), out double d))
                            {
                                //如果能够转换为数值类型，进一步判断数值不为0
                                if (Math.Abs(d) > double.Epsilon)
                                {
                                    converterValue = !converterValue;
                                }
                            }
                            else
                            {
                                converterValue = !converterValue;
                            }
                        }
                    }
                }

                var result = GetResult(targetType, converterValue);
                if (result != null)
                {
                    return result;
                }
            }

            return value;
        }

        /// <summary>
        /// 根据类型获取结果
        /// </summary>
        /// <param name="resultType">目标类型</param>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        internal static object GetResult(Type resultType, bool value)
        {
            if (resultType == typeof(bool) || resultType == typeof(bool?))
            {
                return value;
            }

            if (resultType == typeof(Visibility) || resultType == typeof(Visibility?))
            {
                return value ? Visibility.Visible : Visibility.Collapsed;
            }

            return null;
        }
    }

    public class Item
    {
        public DateTime CreateTime { get; set; }

        public string Content { get; set; }
    }
}
