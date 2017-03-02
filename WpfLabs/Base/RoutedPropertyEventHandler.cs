using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLabs.Base
{
    public delegate void RoutedPropertyEventHandler<T>(object sender, RoutedPropertyEventArgs<T> e);

    public class RoutedPropertyEventArgs<T> : RoutedEventArgs
    {
        private readonly T _value;

        /// <summary>获取由属性更改事件报告的属性的新值。</summary>
        /// <returns>泛型值。 在 <see cref="T:System.Windows.RoutedPropertyChangedEventArgs`1" /> 的实际实现中，此属性的泛型类型将替换为该实现的约束类型。</returns>
        public T Value
        {
            get { return this._value; }
        }

        /// <summary>使用提供的旧值和新值初始化 <see cref="T:System.Windows.RoutedPropertyChangedEventArgs`1" /> 类的新实例。</summary>
        /// <param name="value">属性在引发事件之前的旧值。</param>
        public RoutedPropertyEventArgs(T value)
        {
            this._value = value;
        }

        /// <summary>使用提供的旧值和新值以及事件标识符初始化 <see cref="T:System.Windows.RoutedPropertyChangedEventArgs`1" /> 类的新实例。</summary>
        /// <param name="value">属性在引发事件时的值。</param>
        /// <param name="routedEvent">此参数类带有其信息的路由事件的标识符。</param>
        public RoutedPropertyEventArgs(T value, RoutedEvent routedEvent)
            : this(value)
        {
            this.RoutedEvent = routedEvent;
        }

        /// <summary>以特定于类型的方式调用事件处理程序，这样做可以提高事件系统效率。</summary>
        /// <param name="genericHandler">以特定于类型的方式调用的一般处理程序。</param>
        /// <param name="genericTarget">要对其调用处理程序的目标。</param>
        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((RoutedPropertyEventHandler<T>)genericHandler)(genericTarget, this);
        }
    }
}