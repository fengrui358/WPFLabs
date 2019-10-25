using System;
using System.Collections.ObjectModel;

namespace WpfLabs.DragableListBoxDemo
{
    public class MenuContainer
    {
        public ObservableCollection<MenuUiModel>[] AllMenus { get; set; }

        private Lazy<MenuContainer> _instance = new Lazy<MenuContainer>();

        public MenuContainer Instance => _instance.Value;

        private MenuContainer()
        {

        }
    }
}
