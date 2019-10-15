using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FrHello.NetLib.Core.Windows.Windows;

namespace WpfLabs.ShortCutDemo
{
    /// <summary>
    /// Interaction logic for ShortCutSubWindow.xaml
    /// </summary>
    public partial class ShortCutSubWindow : Window
    {
        private readonly string _windowId;

        public ShortCutSubWindow()
        {
            InitializeComponent();

            _windowId = Guid.NewGuid().ToString("N");
            Title = _windowId;

            if (!WindowsApi.HotKeyHelper.Register(Key.B, ModifierKeys.Control | ModifierKeys.Alt, Handler))
            {
                MessageBox.Show("快捷键注册失败，Ctrl+Alt+B");
            }
            else
            {
                MessageBox.Show("快捷键注册成功，Ctrl+Alt+B");

                //测试移除，如果移除失败则会重复弹出弹窗
                WindowsApi.HotKeyHelper.RegisterOrReplace("test", Key.B, ModifierKeys.Control | ModifierKeys.Alt,
                    Handler);
                WindowsApi.HotKeyHelper.Remove("test");
            }
        }

        private void Handler()
        {
            MessageBox.Show("快捷键触发，Ctrl+Alt+B", _windowId);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            WindowsApi.KeyBoardApi.KeyClick(Key.LeftAlt, Key.LeftCtrl, Key.B);
        }
    }
}