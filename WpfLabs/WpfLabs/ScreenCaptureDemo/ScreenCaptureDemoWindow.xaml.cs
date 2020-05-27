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
using ShareX;

namespace WpfLabs.ScreenCaptureDemo
{
    /// <summary>
    /// ScreenCaptureDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenCaptureDemoWindow : Window
    {
        public ScreenCaptureDemoWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var c = new CaptureRegion(RegionCaptureType.Default);

            var taskSettings = TaskSettings.GetDefaultTaskSettings();
            taskSettings.GeneralSettings.PlaySoundAfterCapture = false;
            taskSettings.GeneralSettings.PlaySoundAfterUpload = false;
            taskSettings.GeneralSettings.PopUpNotification = PopUpNotificationType.None;
            taskSettings.AdvancedSettings.RegionCaptureDisableAnnotation = true;

            c.Capture(taskSettings, true);
            var imageInfo = c.GetLastCapture();

            if (imageInfo != null && imageInfo.Image != null)
            {
                Msg.Text = $"捕获目标，W:{imageInfo.Image.Width}，H:{imageInfo.Image.Height}";
            }
            else
            {
                Msg.Text = "放弃捕获";
            }
        }
    }
}
