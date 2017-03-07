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
using WpfLabs.PeopleResourcePanel.Models;

namespace WpfLabs.PeopleResourcePanel
{
    /// <summary>
    /// PeopleResourcePanelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PeopleResourcePanelWindow : Window
    {
        private List<PeopleModel> _peopleModels;

        public List<PeopleModel> PeopleModels
        {
            get { return _peopleModels; }
            set { _peopleModels = value; }
        }

        public PeopleResourcePanelWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitSource();
        }

        private void InitSource()
        {
            var mockDatas = new List<PeopleModel>();
            mockDatas.Add(new PeopleModel { Name = "david", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "Victoria", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "lily", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "rain", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "david", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "Victoria", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "lily", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "rain", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "david", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "Victoria", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "lily", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });
            mockDatas.Add(new PeopleModel { Name = "rain", PhoneNumber = "010-4578678", Portrait = "Resources/TestPortrait.jpg" });

            PeopleModels = mockDatas;
        }
    }
}
