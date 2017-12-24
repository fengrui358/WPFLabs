using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfLabs.CollectionViewDemo
{
    /// <summary>
    /// CollectionViewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionViewWindow
    {
        private const int Max = 100000;

        public CollectionViewWindow()
        {
            InitializeComponent();

            var itemModels = new List<ItemModel>(Max);
            for (int i = 0; i < Max; i++)
            {
                itemModels.Add(ItemModel.GetNewItem());
            }

            CollectionNormal.ItemModels = itemModels;

            itemModels = new List<ItemModel>(Max);
            for (int i = 0; i < Max; i++)
            {
                itemModels.Add(ItemModel.GetNewItem());
            }

            CollectionView.ItemModels = new ObservableCollection<ItemModel>(itemModels);
        }
    }
}
