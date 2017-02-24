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
using System.Windows.Shapes;
using WpfLabs.ContactsTreeControl.Models;

namespace WpfLabs.ContactsTreeControl
{
    /// <summary>
    /// ContactsTreeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ContactsTreeWindow : Window
    {
        private List<OrganizationModel> _contactsTreeSource;

        public List<OrganizationModel> ContactsTreeSource
        {
            get { return _contactsTreeSource; }
            set { _contactsTreeSource = value; }
        }

        public ContactsTreeWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitSource();
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

        private void ContactsTree_OnShowDetail(object sender, RoutedPropertyChangedEventArgs<PeopleModel> e)
        {
            var oldName = e.OldValue != null ? e.OldValue.Name : "空";
            var newName = e.NewValue.Name;

            MessageBox.Show(string.Format("触发显示详细，旧值：{0}，新值：{1}", oldName, newName));
        }

        private void ContactsTree_OnCallPhone(object sender, RoutedPropertyChangedEventArgs<PeopleModel> e)
        {
            var oldName = e.OldValue != null ? e.OldValue.Name : "空";
            var newName = e.NewValue.Name;

            MessageBox.Show(string.Format("触发打电话，旧值：{0}，新值：{1}", oldName, newName));
        }

        private void ContactsTree_OnSelectedPeopleChanged(object sender, RoutedPropertyChangedEventArgs<PeopleModel> e)
        {
            var oldName = e.OldValue != null ? e.OldValue.Name : "空";
            var newName = e.NewValue.Name;

            MessageBox.Show(string.Format("触发选中，旧值：{0}，新值：{1}", oldName, newName));
        }
    }
}