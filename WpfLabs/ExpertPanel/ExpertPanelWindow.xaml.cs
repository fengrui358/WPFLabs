using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfLabs.ExpertPanel.Models;

namespace WpfLabs.ExpertPanel
{
    /// <summary>
    /// ExpertPanelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExpertPanelWindow : Window
    {
        private List<ExpertModel> _expertsSource;

        public List<ExpertModel> ExpertsSource
        {
            get { return _expertsSource; }
            set { _expertsSource = value; }
        }

        public ExpertPanelWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitSource();
        }

        private void InitSource()
        {
            var mockDatas = new List<ExpertModel>();
            mockDatas.Add(new ExpertModel { Id = 0, Name = "Alan Podemski", Title = "高级犯罪心理分析师", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432"});
            mockDatas.Add(new ExpertModel { Id = 0, Name = "John Smith", Title = "高级刑侦专家", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            mockDatas.Add(new ExpertModel { Id = 0, Name = "Alan Podemski", Title = "高级犯罪心理分析师", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            mockDatas.Add(new ExpertModel { Id = 0, Name = "John Smith", Title = "高级刑侦专家", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            mockDatas.Add(new ExpertModel { Id = 0, Name = "Alan Podemski", Title = "高级犯罪心理分析师", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            mockDatas.Add(new ExpertModel { Id = 0, Name = "John Smith", Title = "高级刑侦专家", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            mockDatas.Add(new ExpertModel { Id = 0, Name = "Alan Podemski", Title = "高级犯罪心理分析师", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            mockDatas.Add(new ExpertModel { Id = 0, Name = "John Smith", Title = "高级刑侦专家", Portrait = "Resources/TestPortrait.jpg", PhoneNumber = "0058-149586432" });
            

            ExpertsSource = mockDatas;
        }
    }
}