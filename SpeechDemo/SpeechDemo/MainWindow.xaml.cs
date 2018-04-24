using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var APP_ID = "8323646";
            var API_KEY = "cHR0DX3unmq1GS65FGI3n7FqiX6klqZE";
            var SECRET_KEY = "oqQd6bD5hatTlYijaGgqyq4Vtt4rFxGr";
            _client = new Tts(API_KEY, SECRET_KEY);


            // 可选参数
            var option = new Dictionary<string, object>()
            {
                {"spd", 5}, // 语速
                {"vol", 7}, // 音量
                {"per", 4}  // 发音人，4：情感度丫丫童声
            };

            var errorCount = 0;
            var successCount = 0;

            var sw = Stopwatch.StartNew();

            var r = Parallel.For(0, 40, async i =>
            {
                await Task.Delay(5000);

                var result = await Synthesis(Guid.NewGuid().ToString("N"), option);
                if (!result.Success)
                {
                    Interlocked.Increment(ref errorCount);
                }
                else
                {
                    Interlocked.Increment(ref successCount);
                }
            });

            var yy = r.IsCompleted;

            sw.Stop();
            var x = sw.ElapsedMilliseconds;
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

                    var result = await Synthesis(Text);
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
    }
}
