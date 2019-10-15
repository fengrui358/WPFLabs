using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace WpfLabs.ShortCutDemo
{
    /// <summary>
    /// Interaction logic for ShortCutDemoWindow.xaml
    /// </summary>
    public partial class ShortCutDemoWindow
    {
        public ShortCutDemoWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var w = new ShortCutSubWindow();
            w.Show();
        }
    }
}
