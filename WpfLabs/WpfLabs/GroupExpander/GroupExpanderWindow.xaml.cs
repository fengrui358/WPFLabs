using System.Collections.Generic;
using System.Windows;
using WpfLabs.GroupExpander.Models;

namespace WpfLabs.GroupExpander
{
    /// <summary>
    /// GroupExpanderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GroupExpanderWindow : Window
    {
        public List<ResourceGroupModel> Datas { get; set; }

        public GroupExpanderWindow()
        {
            var datas = new List<ResourceGroupModel>();

            for (int i = 0; i < 20; i++)
            {
                var group = new ResourceGroupModel
                {
                    GroupName = "GHT",
                    GroupRemark = (3000 + i).ToString(),
                    ResourceGroupSetInfos = new List<ResourceGroupSetModel>()
                };

                for (int j = 0; j < 25; j++)
                {
                    var resource = new ResourceGroupSetModel
                    {
                        Tel = (4000 + j).ToString()
                    };

                    group.ResourceGroupSetInfos.Add(resource);
                }

                datas.Add(group);
            }

            Datas = datas;

            DataContext = this;
            InitializeComponent();
        }
    }
}