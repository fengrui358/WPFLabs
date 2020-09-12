﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using WpfLabs.MasterDetailDataGrid.Models;

namespace WpfLabs.MasterDetailDataGrid
{
    /// <summary>
    /// MasterDetailDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class MasterDetailDataGrid : UserControl
    {
        public static readonly DependencyProperty ItemSourcesProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable<AlarmTransferModel>), typeof(MasterDetailDataGrid),
            new PropertyMetadata(default(IEnumerable<AlarmTransferModel>)));

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<AlarmTransferModel> ItemsSource
        {
            get => (IEnumerable<AlarmTransferModel>) GetValue(ItemSourcesProperty);
            set => SetValue(ItemSourcesProperty, value);
        }

        public AlarmTransferModel SelectedItem { get; set; }

        public RelayCommand TestDoubleClickCommand { get; private set; }

        public MasterDetailDataGrid()
        {
            InitializeComponent();

            TestDoubleClickCommand = new RelayCommand(DoubleClickCommandHandler, SelectedItem != null);
        }

        private void DoubleClickCommandHandler()
        {
            Debug.WriteLine(JsonConvert.SerializeObject(SelectedItem));
        }
    }
}