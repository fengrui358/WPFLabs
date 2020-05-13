using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using GalaSoft.MvvmLight.Command;

namespace WpfLabs.Base
{
    /// <summary>
    /// This <see cref="T:System.Windows.Interactivity.TriggerAction`1" /> can be
    /// used to bind any event on any FrameworkElement to an <see cref="T:System.Windows.Input.ICommand" />.
    /// Typically, this element is used in XAML to connect the attached element
    /// to a command located in a ViewModel. This trigger can only be attached
    /// to a FrameworkElement or a class deriving from FrameworkElement.
    /// <para>To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
    /// and leave the CommandParameter and CommandParameterValue empty!</para>
    /// </summary>
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// Identifies the <see cref="P:EventToCommand.CommandParameter" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(EventToCommand), new PropertyMetadata((object)null, (PropertyChangedCallback)((s, e) =>
        {
            if (!(s is EventToCommand eventToCommand) || eventToCommand.AssociatedObject == null)
                return;
            eventToCommand.EnableDisableElement();
        })));
        /// <summary>
        /// Identifies the <see cref="P:EventToCommand.Command" /> dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventToCommand), new PropertyMetadata((object)null, (PropertyChangedCallback)((s, e) => EventToCommand.OnCommandChanged(s as EventToCommand, e))));
        /// <summary>
        /// Identifies the <see cref="P:EventToCommand.MustToggleIsEnabled" /> dependency property
        /// </summary>
        public static readonly DependencyProperty MustToggleIsEnabledProperty = DependencyProperty.Register(nameof(MustToggleIsEnabled), typeof(bool), typeof(EventToCommand), new PropertyMetadata((object)false, (PropertyChangedCallback)((s, e) =>
        {
            if (!(s is EventToCommand eventToCommand) || eventToCommand.AssociatedObject == null)
                return;
            eventToCommand.EnableDisableElement();
        })));
        /// <summary>
        /// Identifies the <see cref="P:EventToCommand.EventArgsConverterParameter" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EventArgsConverterParameterProperty = DependencyProperty.Register(nameof(EventArgsConverterParameter), typeof(object), typeof(EventToCommand), new PropertyMetadata((PropertyChangedCallback)null));
        /// <summary>
        /// Identifies the <see cref="P:EventToCommand.AlwaysInvokeCommand" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlwaysInvokeCommandProperty = DependencyProperty.Register(nameof(AlwaysInvokeCommand), typeof(bool), typeof(EventToCommand), new PropertyMetadata((object)false));
        private object _commandParameterValue;
        private bool? _mustToggleValue;
        /// <summary>
        /// The <see cref="P:EventToCommand.EventArgsConverterParameter" /> dependency property's name.
        /// </summary>
        public const string EventArgsConverterParameterPropertyName = "EventArgsConverterParameter";
        /// <summary>
        /// The <see cref="P:EventToCommand.AlwaysInvokeCommand" /> dependency property's name.
        /// </summary>
        public const string AlwaysInvokeCommandPropertyName = "AlwaysInvokeCommand";

        /// <summary>
        /// Gets or sets the ICommand that this trigger is bound to. This
        /// is a DependencyProperty.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(EventToCommand.CommandProperty);
            }
            set
            {
                this.SetValue(EventToCommand.CommandProperty, (object)value);
            }
        }

        /// <summary>
        /// Gets or sets an object that will be passed to the <see cref="P:EventToCommand.Command" />
        /// attached to this trigger. This is a DependencyProperty.
        /// </summary>
        public object CommandParameter
        {
            get
            {
                return this.GetValue(EventToCommand.CommandParameterProperty);
            }
            set
            {
                this.SetValue(EventToCommand.CommandParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets an object that will be passed to the <see cref="P:EventToCommand.Command" />
        /// attached to this trigger. This property is here for compatibility
        /// with the Silverlight version. This is NOT a DependencyProperty.
        /// For databinding, use the <see cref="P:EventToCommand.CommandParameter" /> property.
        /// </summary>
        public object CommandParameterValue
        {
            get
            {
                return this._commandParameterValue ?? this.CommandParameter;
            }
            set
            {
                this._commandParameterValue = value;
                this.EnableDisableElement();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the attached element must be
        /// disabled when the <see cref="P:EventToCommand.Command" /> property's CanExecuteChanged
        /// event fires. If this property is true, and the command's CanExecute
        /// method returns false, the element will be disabled. If this property
        /// is false, the element will not be disabled when the command's
        /// CanExecute method changes. This is a DependencyProperty.
        /// </summary>
        public bool MustToggleIsEnabled
        {
            get
            {
                return (bool)this.GetValue(EventToCommand.MustToggleIsEnabledProperty);
            }
            set
            {
                this.SetValue(EventToCommand.MustToggleIsEnabledProperty, (object)value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the attached element must be
        /// disabled when the <see cref="P:EventToCommand.Command" /> property's CanExecuteChanged
        /// event fires. If this property is true, and the command's CanExecute
        /// method returns false, the element will be disabled. This property is here for
        /// compatibility with the Silverlight version. This is NOT a DependencyProperty.
        /// For databinding, use the <see cref="P:EventToCommand.MustToggleIsEnabled" /> property.
        /// </summary>
        public bool MustToggleIsEnabledValue
        {
            get
            {
                return this._mustToggleValue.HasValue ? this._mustToggleValue.Value : this.MustToggleIsEnabled;
            }
            set
            {
                this._mustToggleValue = new bool?(value);
                this.EnableDisableElement();
            }
        }

        /// <summary>
        /// Called when this trigger is attached to a FrameworkElement.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.EnableDisableElement();
        }

        /// <summary>
        /// This method is here for compatibility
        /// with the Silverlight version.
        /// </summary>
        /// <returns>The FrameworkElement to which this trigger
        /// is attached.</returns>
        private FrameworkElement GetAssociatedObject()
        {
            return this.AssociatedObject as FrameworkElement;
        }

        /// <summary>
        /// This method is here for compatibility
        /// with the Silverlight 3 version.
        /// </summary>
        /// <returns>The command that must be executed when
        /// this trigger is invoked.</returns>
        private ICommand GetCommand()
        {
            return this.Command;
        }

        /// <summary>
        /// Specifies whether the EventArgs of the event that triggered this
        /// action should be passed to the bound RelayCommand. If this is true,
        /// the command should accept arguments of the corresponding
        /// type (for example RelayCommand&lt;MouseButtonEventArgs&gt;).
        /// </summary>
        public bool PassEventArgsToCommand { get; set; }

        /// <summary>
        /// Gets or sets a converter used to convert the EventArgs when using
        /// <see cref="P:EventToCommand.PassEventArgsToCommand" />. If PassEventArgsToCommand is false,
        /// this property is never used.
        /// </summary>
        public IEventArgsConverter EventArgsConverter { get; set; }

        /// <summary>
        /// Gets or sets a parameters for the converter used to convert the EventArgs when using
        /// <see cref="P:EventToCommand.PassEventArgsToCommand" />. If PassEventArgsToCommand is false,
        /// this property is never used. This is a dependency property.
        /// </summary>
        public object EventArgsConverterParameter
        {
            get
            {
                return this.GetValue(EventToCommand.EventArgsConverterParameterProperty);
            }
            set
            {
                this.SetValue(EventToCommand.EventArgsConverterParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the command should be invoked even
        /// if the attached control is disabled. This is a dependency property.
        /// </summary>
        public bool AlwaysInvokeCommand
        {
            get
            {
                return (bool)this.GetValue(EventToCommand.AlwaysInvokeCommandProperty);
            }
            set
            {
                this.SetValue(EventToCommand.AlwaysInvokeCommandProperty, (object)value);
            }
        }

        /// <summary>
        /// Provides a simple way to invoke this trigger programatically
        /// without any EventArgs.
        /// </summary>
        public void Invoke()
        {
            this.Invoke((object)null);
        }

        /// <summary>
        /// Executes the trigger.
        /// <para>To access the EventArgs of the fired event, use a RelayCommand&lt;EventArgs&gt;
        /// and leave the CommandParameter and CommandParameterValue empty!</para>
        /// </summary>
        /// <param name="parameter">The EventArgs of the fired event.</param>
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedElementIsDisabled() && !this.AlwaysInvokeCommand)
                return;
            ICommand command = this.GetCommand();
            object parameter1 = this.CommandParameterValue;
            if (parameter1 == null && this.PassEventArgsToCommand)
                parameter1 = this.EventArgsConverter == null ? parameter : this.EventArgsConverter.Convert(parameter, this.EventArgsConverterParameter);
            if (command == null || !command.CanExecute(parameter1))
                return;
            command.Execute(parameter1);
        }

        private static void OnCommandChanged(
          EventToCommand element,
          DependencyPropertyChangedEventArgs e)
        {
            if (element == null)
                return;
            if (e.OldValue != null)
                ((ICommand)e.OldValue).CanExecuteChanged -= new EventHandler(element.OnCommandCanExecuteChanged);
            ICommand newValue = (ICommand)e.NewValue;
            if (newValue != null)
                newValue.CanExecuteChanged += new EventHandler(element.OnCommandCanExecuteChanged);
            element.EnableDisableElement();
        }

        private bool AssociatedElementIsDisabled()
        {
            FrameworkElement associatedObject = this.GetAssociatedObject();
            if (this.AssociatedObject == null)
                return true;
            return associatedObject != null && !associatedObject.IsEnabled;
        }

        private void EnableDisableElement()
        {
            FrameworkElement associatedObject = this.GetAssociatedObject();
            if (associatedObject == null)
                return;
            ICommand command = this.GetCommand();
            if (!this.MustToggleIsEnabledValue || command == null)
                return;
            associatedObject.IsEnabled = command.CanExecute(this.CommandParameterValue);
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            this.EnableDisableElement();
        }
    }
}
