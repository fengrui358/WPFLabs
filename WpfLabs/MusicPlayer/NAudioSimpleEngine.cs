using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;
using NAudio.Wave;
using WPFSoundVisualizationLib;

namespace WpfLabs.MusicPlayer
{
    /// <summary>
    /// 精简的播放引擎，能够播放MP3和Wav，并且可以获取音符数据
    /// </summary>
    public class NAudioSimpleEngine : INotifyPropertyChanged, ISpectrumPlayer, IDisposable
    {
        #region 字段

        private readonly object _lockChannelSet = new object();
        private readonly DispatcherTimer _updateChannelPositionTimer;

        private WaveOut _waveOutDevice;
        private WaveStream _activeStream;
        private WaveChannel32 _inputStream;
        private readonly int _fftDataSize = (int)FFTDataSize.FFT2048;
        private SampleAggregator _sampleAggregator;

        private string _currentFilePath;

        private bool _disposed;
        private static Lazy<NAudioSimpleEngine> _instance = new Lazy<NAudioSimpleEngine>(() => new NAudioSimpleEngine(), LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion

        #region 属性

        public static NAudioSimpleEngine Instance
        {
            get { return _instance.Value; }
        }

        /// <summary>
        /// 是否允许播放
        /// </summary>
        public bool CanPlay
        {
            get
            {
                return !string.IsNullOrEmpty(_currentFilePath) && File.Exists(_currentFilePath) &&
                       _waveOutDevice != null && _activeStream != null && _inputStream != null;
            }
        }

        /// <summary>
        /// 是否允许暂停
        /// </summary>
        public bool CanPause
        {
            get { return _waveOutDevice != null && _waveOutDevice.PlaybackState == PlaybackState.Playing; }
        }

        /// <summary>
        /// 是否允许停止
        /// </summary>
        public bool CanStop
        {
            get
            {
                return _waveOutDevice != null &&
                       (_waveOutDevice.PlaybackState == PlaybackState.Playing ||
                        _waveOutDevice.PlaybackState == PlaybackState.Paused);
            }
        }

        /// <summary>
        /// 当前文件流的长度，即总秒数
        /// </summary>
        public double ChannelLength
        {
            get
            {
                var length = 0d;
                if (_activeStream != null)
                {
                    length = _activeStream.TotalTime.TotalSeconds;
                }

                return length;
            }
        }

        /// <summary>
        /// 当前播放位置
        /// </summary>
        public double ChannelPosition
        {
            get
            {
                if (_activeStream != null)
                {
                    return ((double)_activeStream.Position / (double)_activeStream.Length) * _activeStream.TotalTime.TotalSeconds;
                }

                return 0d;
            }
            set
            {
                if (CanPlay)
                {
                    lock (_lockChannelSet)
                    {
                        double oldValue = ((double)_activeStream.Position / (double)_activeStream.Length) *
                                          _activeStream.TotalTime.TotalSeconds;
                        double position = Math.Max(0, Math.Min(value, ChannelLength));

                        if (oldValue != position)
                        {
                            _activeStream.Position =
                                (long)((position / _activeStream.TotalTime.TotalSeconds) * _activeStream.Length);
                            OnPropertyChanged();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return _waveOutDevice != null && _waveOutDevice.PlaybackState == PlaybackState.Playing; }
        }

        #endregion

        #region 构造

        private NAudioSimpleEngine()
        {
            _updateChannelPositionTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            _updateChannelPositionTimer.Interval = TimeSpan.FromMilliseconds(50);
            _updateChannelPositionTimer.Tick += ChangeChannelPosition;
        }

        #endregion

        #region ISpectrumPlayer接口实现

        public bool GetFFTData(float[] fftDataBuffer)
        {
            _sampleAggregator.GetFFTResults(fftDataBuffer);
            return IsPlaying;
        }

        public int GetFFTFrequencyIndex(int frequency)
        {
            double maxFrequency;
            if (_activeStream != null)
            {
                maxFrequency = _activeStream.WaveFormat.SampleRate / 2.0d;
            }
            else
            {
                maxFrequency = 22050; // Assume a default 44.1 kHz sample rate.
            }
            return (int) ((frequency / maxFrequency) * (_fftDataSize / 2));
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public bool OpenFile(string filePath)
        {
            //如果播放文件一样，没必要继续操作
            if (_currentFilePath == filePath)
            {
                return false;
            }

            //如果播放文件不存在直接退出
            if (!File.Exists(filePath))
            {
                return false;
            }

            StopAndCloseStream();

            try
            {
                _waveOutDevice = new WaveOut()
                {
                    DesiredLatency = 100
                };

                if (GetTypeWithExtension(filePath) == AudioFileType.Mp3)
                {
                    _activeStream = new Mp3FileReader(filePath);
                }
                else if(GetTypeWithExtension(filePath) == AudioFileType.Wav)
                {
                    _activeStream = new WaveFileReader(filePath);
                }
                else
                {
                    return false;
                }

                _inputStream = new WaveChannel32(_activeStream);

                _sampleAggregator = new SampleAggregator(_fftDataSize);
                _inputStream.Sample += InputStreamOnSampleChanged;
                _waveOutDevice.Init(_inputStream);
                _currentFilePath = filePath;

                RefreshPlayingState();

                return true;
            }
            catch(Exception e)
            {
                _activeStream = null;
                Debug.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 校验文件路径是否可能播放
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public bool VerifyFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return false;
            }

            var extension = GetTypeWithExtension(filePath);
            return extension != AudioFileType.Other;
        }

        /// <summary>
        /// 停止并关闭文件
        /// </summary>
        public void CloseFile()
        {
            StopAndCloseStream();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (CanStop)
            {
                _waveOutDevice.Stop();
                _activeStream.Position = 0;

                RefreshPlayingState();
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            if (IsPlaying && CanPause)
            {
                _waveOutDevice.Pause();
                RefreshPlayingState();
            }
        }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            if (CanPlay && !IsPlaying)
            {
                _waveOutDevice.Play();
                _updateChannelPositionTimer.Start();

                RefreshPlayingState();
            }
        }

        #endregion

        #region 私有方法

        #region 刷新UI

        //为了避免频繁刷新UI，这里加入缓存字段判断是否需要刷新
        private bool _isPlayingLastState;
        private bool _canPause;
        private bool _canPlay;
        private bool _canStop;
        private double _channelLength;
        private double _channelPosition;

        /// <summary>
        /// 刷新播放状态
        /// </summary>
        private void RefreshPlayingState()
        {
            if (_isPlayingLastState != IsPlaying)
            {
                _isPlayingLastState = IsPlaying;
                OnPropertyChanged("IsPlaying");
            }

            if (_canPause != CanPause)
            {
                _canPause = CanPause;
                OnPropertyChanged("CanPause");
            }

            if (_canPlay != CanPlay)
            {
                _canPlay = CanPlay;
                OnPropertyChanged("CanPlay");
            }

            if (_canStop != CanStop)
            {
                _canStop = CanStop;
                OnPropertyChanged("CanStop");
            }

            if (_channelLength != ChannelLength)
            {
                _channelLength = ChannelLength;
                OnPropertyChanged("ChannelLength");
            }

            if (_channelPosition != ChannelPosition)
            {
                _channelPosition = ChannelPosition;
                OnPropertyChanged("ChannelPosition");
            }
        }

        #endregion

        private void StopAndCloseStream()
        {
            if (CanStop)
            {
                _waveOutDevice.Stop();
            }
            _currentFilePath = String.Empty;

            if (_inputStream != null)
            {
                _inputStream.Dispose();
                _inputStream = null;
            }

            if (_activeStream != null)
            {
                _activeStream.Dispose();
                _activeStream = null;
            }

            if (_waveOutDevice != null)
            {
                _waveOutDevice.Dispose();
                _waveOutDevice = null;
            }

            RefreshPlayingState();
        }

        private AudioFileType GetTypeWithExtension(string path)
        {
            var result = AudioFileType.Other;

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                var extension = Path.GetExtension(path);

                if (extension.Equals(".mp3", StringComparison.InvariantCultureIgnoreCase))
                {
                    result = AudioFileType.Mp3;
                }
                else if (extension.Equals(".wav", StringComparison.InvariantCultureIgnoreCase))
                {
                    result = AudioFileType.Wav;
                }
            }

            return result;
        }

        private void InputStreamOnSampleChanged(object sender, SampleEventArgs sampleEventArgs)
        {
            _sampleAggregator.Add(sampleEventArgs.Left, sampleEventArgs.Right);
        }

        /// <summary>
        /// 改变流位置
        /// </summary>
        private void ChangeChannelPosition(object sender, EventArgs e)
        {
            OnPropertyChanged("ChannelPosition");

            if (!IsPlaying)
            {
                _updateChannelPositionTimer.Stop();
            }

            //播放结束需要暂停
            if (_activeStream != null && _activeStream.CurrentTime >= _activeStream.TotalTime)
            {
                Stop();
            }
        }

        #endregion

        #region IDisposable接口实现

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    StopAndCloseStream();
                }

                _disposed = true;
            }
        }

        ~NAudioSimpleEngine()
        {
            Dispose(false);
        }

        #endregion

        #endregion

        #region INotifyPropertyChanged接口实现

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region 私有枚举

        private enum AudioFileType
        {
            Mp3,

            Wav,

            Other
        }

        #endregion
    }
}
