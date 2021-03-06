﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLabs.MusicPlayer
{
    /// <summary>
    /// MusicPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class MusicPlayer : UserControl
    {
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(
            "FilePath", typeof(string), typeof(MusicPlayer),
            new PropertyMetadata(default(string), ResourceChangedCallback));

        /// <summary>
        /// 待播放的文件路径
        /// </summary>
        public string FilePath
        {
            get { return (string) GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public static readonly DependencyProperty AutoPlayProperty = DependencyProperty.Register(
            "AutoPlay", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(true));

        /// <summary>
        /// 自动播放，当音频文件准备好后立即自动播放
        /// </summary>
        public bool AutoPlay
        {
            get { return (bool) GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            "IsPlaying", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(default(bool)));

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool) GetValue(IsPlayingProperty); }
            private set { SetValue(IsPlayingProperty, value); }
        }

        public static readonly DependencyProperty CanPlayProperty = DependencyProperty.Register(
            "CanPlay", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(default(bool)));

        /// <summary>
        /// 是否允许播放
        /// </summary>
        public bool CanPlay
        {
            get { return (bool) GetValue(CanPlayProperty); }
            private set { SetValue(CanPlayProperty, value); }
        }

        public static readonly DependencyProperty ChannelPositionProperty = DependencyProperty.Register(
            "ChannelPosition", typeof(double), typeof(MusicPlayer),
            new PropertyMetadata(default(double), ChannelPositionCallback));

        private static void ChannelPositionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!NAudioSimpleEngine.Instance.CanPlay)
            {
                return;
            }

            //此处做个算法，如果新值大于旧值，且范围在0.5以内，则认为是程序自动播放更改值，则不做任何处理，否则是人为变化，需要暂停播放，然后重置
            if (Math.Abs((double)dependencyPropertyChangedEventArgs.NewValue - (double)dependencyPropertyChangedEventArgs.OldValue) > 0.5)
            {
                if (NAudioSimpleEngine.Instance.IsPlaying)
                {
                    NAudioSimpleEngine.Instance.Pause();
                    NAudioSimpleEngine.Instance.ChannelPosition = (double)dependencyPropertyChangedEventArgs.NewValue;
                    NAudioSimpleEngine.Instance.Play();
                }
                else
                {
                    NAudioSimpleEngine.Instance.ChannelPosition = (double)dependencyPropertyChangedEventArgs.NewValue;
                }
            }
        }

        /// <summary>
        /// 当前播放位置
        /// </summary>
        public double ChannelPosition
        {
            get { return (double) GetValue(ChannelPositionProperty); }
            private set { SetValue(ChannelPositionProperty, value); }
        }

        public static readonly DependencyProperty ChannelLengthProperty = DependencyProperty.Register(
            "ChannelLength", typeof(double), typeof(MusicPlayer), new PropertyMetadata(100d));

        /// <summary>
        /// 音频总长度
        /// </summary>
        public double ChannelLength
        {
            get { return (double) GetValue(ChannelLengthProperty); }
            private set { SetValue(ChannelLengthProperty, value); }
        }

        public MusicPlayer()
        {
            InitializeComponent();

            NAudioSimpleEngine.Instance.PropertyChanged += NAudioSimpleEngine_PropertyChanged;
            SpectrumAnalyzer.RegisterSoundPlayer(NAudioSimpleEngine.Instance);
        }

        private void NAudioSimpleEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsPlaying":
                    IsPlaying = NAudioSimpleEngine.Instance.IsPlaying;
                    break;
                case "ChannelPosition":
                    ChannelPosition = NAudioSimpleEngine.Instance.ChannelPosition;
                    break;
                case "ChannelLength":
                    ChannelLength = NAudioSimpleEngine.Instance.ChannelLength;
                    break;
                case "CanPlay":
                    CanPlay = NAudioSimpleEngine.Instance.CanPlay;
                    break;
            }
        }

        private static void ResourceChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var player = (MusicPlayer) dependencyObject;

            if (dependencyPropertyChangedEventArgs.NewValue != null &&
                dependencyPropertyChangedEventArgs.NewValue != dependencyPropertyChangedEventArgs.OldValue)
            {
                var filePath = dependencyPropertyChangedEventArgs.NewValue.ToString();
                if (NAudioSimpleEngine.Instance.VerifyFilePath(filePath))
                {
                    NAudioSimpleEngine.Instance.OpenFile(filePath);

                    if (player.AutoPlay && NAudioSimpleEngine.Instance.CanPlay)
                    {
                        NAudioSimpleEngine.Instance.Play();
                    }
                }
            }
        }

        private void MusicPlayer_OnUnloaded(object sender, RoutedEventArgs e)
        {
            NAudioSimpleEngine.Instance.Stop();

            //清理之前打开的文件
            NAudioSimpleEngine.Instance.CloseFile();
        }

        private void PlayBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NAudioSimpleEngine.Instance.Play();
        }

        private void PauseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NAudioSimpleEngine.Instance.Pause();
        }

        private void ForwardBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var currentPosition = ChannelPosition;
            var newPosition = (currentPosition + 5) > ChannelLength ? ChannelLength : (currentPosition + 5);

            ChannelPosition = newPosition;
        }

        private void Backwards_OnClick(object sender, RoutedEventArgs e)
        {
            var currentPosition = ChannelPosition;
            var newPosition = (currentPosition - 5) < 0 ? 0 : (currentPosition - 5);

            ChannelPosition = newPosition;
        }
    }
}