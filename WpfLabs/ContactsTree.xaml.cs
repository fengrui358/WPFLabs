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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLabs.Models;

namespace WpfLabs
{
    /// <summary>
    /// ContactsTree.xaml 的交互逻辑
    /// </summary>
    public partial class ContactsTree : UserControl
    {
        public ContactsTree()
        {
            InitializeComponent();

            InitSource();

            DataContext = this;
        }

        public List<OrganizationModel> ContactsTreeSource { get; set; }

        private void InitSource()
        {
            var mockDatas = new List<OrganizationModel>();

            for (int i = 0; i < 2; i++)
            {
                var organizationModel = new OrganizationModel { Icon = "Resources/Telephone.png", Name = "组织" + Guid.NewGuid() };

                for (int j = 0; j < 2; j++)
                {
                    var childOrganizationModel = new OrganizationModel { Icon = "Resources/Telephone.png", Name = "组织" + Guid.NewGuid() };

                    for (int k = 0; k < 2; k++)
                    {
                        var peopleModel = new PeopleModel();
                        peopleModel.Name = "人员" + Guid.NewGuid();

                        childOrganizationModel.PeopleChildren.Add(peopleModel);
                    }

                    organizationModel.OrganizationChildren.Add(childOrganizationModel);
                }

                mockDatas.Add(organizationModel);
            }

            ContactsTreeSource = mockDatas;
        }
    }
}
