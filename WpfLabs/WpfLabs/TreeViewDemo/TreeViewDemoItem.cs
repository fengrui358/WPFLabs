using System.Collections.Generic;

namespace WpfLabs.TreeViewDemo
{
    public class TreeViewDemoItem
    {
        public string Name { get; set; }

        public List<TreeViewDemoItem> Children { get; set; }
    }
}
