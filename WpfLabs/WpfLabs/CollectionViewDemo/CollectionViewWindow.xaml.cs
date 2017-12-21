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
        private const int Max = 10000;
        public List<ItemModel> ItemModels { get; set; }

        public CollectionViewWindow()
        {
            ItemModels = new List<ItemModel>(Max);
            for (int i = 0; i < Max; i++)
            {
                ItemModels.Add(ItemModel.GetNewItem());
            }

            InitializeComponent();

            CollectionNormal.ItemModels = ItemModels;
        }
    }
}
