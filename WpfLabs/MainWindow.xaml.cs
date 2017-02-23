using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLabs.Models;

namespace WpfLabs
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<OrganizationModel> _contactsTreeSource;

        public List<OrganizationModel> ContactsTreeSource
        {
            get { return _contactsTreeSource; }
            set
            {
                _contactsTreeSource = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitSource();

        }

        private void ContactsTree_OnSelectedPeople(PeopleModel obj)
        {
            MessageBox.Show("选中" + obj.Name);
        }

        private void InitSource()
        {
            var mockDatas = new List<OrganizationModel>();

            for (int i = 0; i < 2; i++)
            {
                var organizationModel = new OrganizationModel
                {
                    Icon = "Resources/Telephone.png",
                    Name = "组织" + Guid.NewGuid()
                };

                for (int j = 0; j < 2; j++)
                {
                    var childOrganizationModel = new OrganizationModel
                    {
                        Icon = "Resources/Telephone.png",
                        Name = "组织" + Guid.NewGuid()
                    };

                    for (int k = 0; k < 2; k++)
                    {
                        var peopleModel = new PeopleModel();
                        if (k % 2 == 0)
                        {
                            peopleModel.Name = "人员" + Guid.NewGuid();
                        }
                        else
                        {
                            peopleModel.Name = "人员" + Guid.NewGuid() + Guid.NewGuid();
                        }

                        childOrganizationModel.PeopleChildren.Add(peopleModel);
                    }

                    organizationModel.OrganizationChildren.Add(childOrganizationModel);
                }

                mockDatas.Add(organizationModel);
            }

            ContactsTreeSource = mockDatas;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ContactsTree_OnShowDetail(PeopleModel obj)
        {
            MessageBox.Show("显示详细" + obj.Name);
        }

        private void ContactsTree_OnCallPhone(PeopleModel obj)
        {
            MessageBox.Show("打电话" + obj.Name);
        }
    }
}
