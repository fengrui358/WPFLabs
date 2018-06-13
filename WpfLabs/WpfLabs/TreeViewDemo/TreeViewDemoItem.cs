using System;
using System.Collections.Generic;

namespace WpfLabs.TreeViewDemo
{
    public class TreeViewDemoItem
    {
        public string Name { get; set; }

        public List<TreeViewDemoItem> Children { get; set; }

        public TreeViewDemoItem()
        {
            Name = Guid.NewGuid().ToString("N");
            Children = new List<TreeViewDemoItem>();
        }
    }
}
