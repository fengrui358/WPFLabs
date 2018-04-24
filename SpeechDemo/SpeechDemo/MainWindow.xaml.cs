using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Baidu.Aip.Speech;

namespace SpeechDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }
        private Tts _client;
        private readonly MediaPlayer _player = new MediaPlayer();

        private bool _isBusying;

        /// <summary>
        /// 是否在忙碌状态
        /// </summary>
        public bool IsBusying
        {
            get => _isBusying;
            set
            {
                if (_isBusying != value)
                {
                    _isBusying = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _spd = 5;
        /// <summary>
        /// 语速
        /// </summary>
        public int Spd
        {
            get => _spd;
            set => _spd = value;
        }

        private int _pit = 5;
        /// <summary>
        /// 音调
        /// </summary>
        public int Pit
        {
            get => _pit;
            set => _pit = value;
        }

        private int _vol = 5;
        /// <summary>
        /// 音量
        /// </summary>
        public int Vol
        {
            get => _vol;
            set => _vol = value;
        }

        /// <summary>
        /// 发音人选择, 0为女声，1为男声，3为情感合成-度逍遥，4为情感合成-度丫丫，默认为普通女
        /// </summary>
        private int _per;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];

            _client = new Tts(apiKey, secretKey);
        }

        private async Task<TtsResponse> Synthesis(string text, Dictionary<string, object> options = null)
        {
            return await Task.Run(() =>
            {
                var result = _client.Synthesis(text, options);
                return result;
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// 朗读
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReadBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    IsBusying = true;

                    // 可选参数
                    var option = new Dictionary<string, object>()
                    {
                        {"spd", Spd}, // 语速
                        {"vol", Vol}, // 音量
                        {"pit", Pit },//音调
                        {"per", _per}  // 发音人，4：情感度丫丫童声
                    };

                    var result = await Synthesis(Text, option);
                    if (result.Success)
                    {
                        _player.Stop();
                        _player.Close();

                        var mp3FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Last.mp3");
                        File.WriteAllBytes(mp3FileName, result.Data);

                        if (File.Exists(mp3FileName))
                        {
                            _player.Open(new Uri(mp3FileName));
                            _player.Play();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{result.ErrorCode}{result.ErrorMsg}", "错误", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusying = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainWindow_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _player.Stop();
            _player.Close();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement) sender;
            _per = int.Parse(frameworkElement.Tag.ToString());
        }
    }
}
