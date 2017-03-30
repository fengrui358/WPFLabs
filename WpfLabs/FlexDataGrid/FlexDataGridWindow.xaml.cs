using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfLabs.FlexDataGrid.Models;

namespace WpfLabs.FlexDataGrid
{
    /// <summary>
    /// FlexDataGridWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FlexDataGridWindow : Window
    {
        public ObservableCollection<FlexDataGridItemModel> FlexDataGridItemModels { get; set; }

        public FlexDataGridWindow()
        {
            InitializeComponent();

            InitSource();
            DataContext = this;
        }

        private void InitSource()
        {
            var datas = new ObservableCollection<FlexDataGridItemModel>();

            for (int i = 0; i < 20; i++)
            {
                var masterModel = GetNewModel();

                datas.Add(masterModel);
            }

            FlexDataGridItemModels = datas;
        }

        private static readonly Random Random = new Random();

        private FlexDataGridItemModel GetNewModel()
        {
            var model = new FlexDataGridItemModel
            {
                Type = Random.Next(0, 5),
                Name = "Police car-11",
                Contacts = "justin Timberlake",
                DispatchTime = DateTime.Now.Subtract(TimeSpan.FromDays(Random.Next(2, 8)))
            };

            return model;
        }
    }
}
