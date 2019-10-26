using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows;

namespace WpfLabs.DragableListBoxDemo
{
    public class MenuContainer
    {
        private System.Threading.Timer _timer;

        private object _async = new object();

        private Dictionary<int, List<MenuUiModel>> _menus = new Dictionary<int, List<MenuUiModel>>();

        private static readonly Lazy<MenuContainer> _instance = new Lazy<MenuContainer>();

        public static MenuContainer Instance => _instance.Value;

        private MenuContainer()
        {
            _timer = new System.Threading.Timer(s =>
            {
                lock (_async)
                {
                    
                }
            }, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Register(DragableListBoxDemo dragableListBoxDemo)
        {
            dragableListBoxDemo.Menus.CollectionChanged += MenusOnCollectionChanged;
        }

        public void Register(DragableListBoxDemo2 dragableListBoxDemo2)
        {
            dragableListBoxDemo2.Menus.CollectionChanged += MenusOnCollectionChanged;
        }

        private void MenusOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            lock (_async)
            {
                var fw = (FrameworkElement) sender;
                var orderIndex = (int) fw.Tag;

                if (!_menus.ContainsKey(orderIndex))
                {
                    _menus.Add(orderIndex, new List<MenuUiModel>());
                }

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        for (var i = e.NewItems.Count - 1; i >= 0; i--)
                        {
                            _menus[i].Insert(e.NewStartingIndex, (MenuUiModel) e.NewItems[i]);
                        }
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        
                        break;
                }
            }

            _timer.Change(TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
        }
    }
}
