using System;
using System.Collections.ObjectModel;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using Newtonsoft.Json;
using NLog;

namespace WpfLabs.DragableTabControlDemo
{
    /// <summary>
    /// DragableTabControlDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DragableTabControlDemo : Window, IDropTarget, IDragSource
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public ObservableCollection<DragableTabItemModel> LeftItems { get; set; }
        public ObservableCollection<DragableTabItemModel> RightItems { get; set; }

        public DragableTabControlDemo()
        {
            InitializeComponent();
            LeftItems = new ObservableCollection<DragableTabItemModel>();
            RightItems = new ObservableCollection<DragableTabItemModel>();

            DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dragableTabControlDemo = new DragableTabControlDemo();
            dragableTabControlDemo.Show();
        }

        private void DragableTabControlDemo_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                LeftItems.Add(new DragableTabItemModel {Name = $"Left {i + 1}"});
                RightItems.Add(new DragableTabItemModel {Name = $"Right {i + 1}"});
                //LeftTabControl.Items.Add(new TabItem {Header = $"Left{i + 1}"});
                //RightTabControl.Items.Add(new TabItem { Header = $"Right{i + 1}" });
            }
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            //_logger.Info(JsonConvert.SerializeObject(dropInfo));
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            //_logger.Info(JsonConvert.SerializeObject(dropInfo));
        }

        void IDragSource.StartDrag(IDragInfo dragInfo)
        {
            //_logger.Info(JsonConvert.SerializeObject(dragInfo));
        }

        bool IDragSource.CanStartDrag(IDragInfo dragInfo)
        {
            //_logger.Info(JsonConvert.SerializeObject(dragInfo));
            return true;
        }

        void IDragSource.Dropped(IDropInfo dropInfo)
        {
            //_logger.Info(JsonConvert.SerializeObject(dropInfo));
        }

        void IDragSource.DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            //_logger.Info($"{nameof(DragDropEffects)}:{operationResult},{JsonConvert.SerializeObject(dragInfo)}");
        }

        void IDragSource.DragCancelled()
        {
            //_logger.Info(nameof(IDragSource.DragCancelled));
        }

        bool IDragSource.TryCatchOccurredException(Exception exception)
        {
            //_logger.Info(exception);
            return true;
        }
    }
}