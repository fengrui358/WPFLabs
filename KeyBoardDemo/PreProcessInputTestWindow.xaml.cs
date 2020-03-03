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
using System.Windows.Shapes;

namespace KeyBoardDemo
{
    /// <summary>
    /// PreProcessInputTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PreProcessInputTestWindow : Window
    {
        public PreProcessInputTestWindow()
        {
            InitializeComponent();
        }

        private void PreProcessInputTestWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            InputManager.Current.PreProcessInput += Current_PreProcessInput;
        }

        private void Current_PreProcessInput(object sender, PreProcessInputEventArgs args)
        {
            try
            {
                if (args != null && args.StagingItem != null && args.StagingItem.Input != null)
                {
                    InputEventArgs inputEvent = args.StagingItem.Input;

                    if (inputEvent is KeyboardEventArgs)
                    {
                        KeyboardEventArgs k = inputEvent as KeyboardEventArgs;
                        RoutedEvent r = k.RoutedEvent;
                        KeyEventArgs keyEvent = k as KeyEventArgs;

                        if (r == Keyboard.KeyDownEvent)
                        {
                            InputTxt.Text +=
                                $"按下:{keyEvent?.Key} Modifiers:{k.KeyboardDevice.Modifiers} SystemKey:{keyEvent?.SystemKey}{Environment.NewLine}";

                            InputTxt.Text +=
                                $"确定真实按键:{k.KeyboardDevice.Modifiers} {GetRealKey(keyEvent.Key, keyEvent.SystemKey)}{Environment.NewLine}";
                        }

                        if (r == Keyboard.KeyUpEvent)
                        {
                            InputTxt.Text +=
                                $"抬起:{keyEvent?.Key} Modifiers:{k.KeyboardDevice.Modifiers} SystemKey:{keyEvent?.SystemKey}{Environment.NewLine}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InputTxt.Text = ex.ToString();
            }
        }

        private Key GetRealKey(Key? key, Key? systemKey)
        {
            var result = Key.None;

            if (key != null)
            {
                if (key == Key.System)
                {
                    result = systemKey ?? Key.None;
                }
                else
                {
                    result = key.Value;
                }
            }

            return result;
        }
    }
}