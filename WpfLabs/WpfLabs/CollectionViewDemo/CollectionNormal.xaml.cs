using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfLabs.CollectionViewDemo
{
    /// <summary>
    /// CollectionNormal.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionNormal : INotifyPropertyChanged
    {
        private Stopwatch _stopwatch = new Stopwatch();
        public List<ItemModel> ItemModels { get; set; }

        private ObservableCollection<ItemModel> _displayItemModels;

        public ObservableCollection<ItemModel> DisplayItemModels
        {
            get => _displayItemModels;
            set
            {
                _displayItemModels = value;
                OnPropertyChanged();
            }
        }

        public string Key { get; set; }

        private long _elapsedMilliseconds;

        public long ElapsedMilliseconds
        {
            get => _elapsedMilliseconds;
            set
            {
                _elapsedMilliseconds = value;
                OnPropertyChanged();
            }
        }

        public CollectionNormal()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();

            Refresh();

            _stopwatch.Stop();
            ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();

            var newItem = ItemModel.GetNewItem();
            ItemModels.Insert(0, newItem);
            DisplayItemModels.Insert(0, newItem);

            _stopwatch.Stop();
            ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        private void Sub_OnClick(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();

            if (ItemModels.Any())
            {
                ItemModels.RemoveAt(0);
                DisplayItemModels.RemoveAt(0);
            }

            _stopwatch.Stop();
            ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        private void Up_OnClick(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();

            if (ItemModels.Any())
            {
                ItemModels = ItemModels.OrderBy(s => s.Name).ToList();
                Refresh();
            }

            _stopwatch.Stop();
            ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        private void Down_OnClick(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();

            if (ItemModels.Any())
            {
                ItemModels = ItemModels.OrderByDescending(s => s.Name).ToList();
                Refresh();
            }

            _stopwatch.Stop();
            ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        private void Refresh()
        {
            if (!string.IsNullOrEmpty(Key))
            {
                DisplayItemModels = new ObservableCollection<ItemModel>(ItemModels.Where(s => s.Name.Contains(Key)));
            }
            else
            {
                DisplayItemModels = new ObservableCollection<ItemModel>(ItemModels);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
