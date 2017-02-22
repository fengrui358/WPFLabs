using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfLabs.Models
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class OrganizationModel : ObservableObject
    {
        private long _id;
        private string _icon;
        private string _name;
        private List<OrganizationModel> _organizationChildren;
        private List<PeopleModel> _peopleModel;

        private long Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        public string Icon
        {
            get { return _icon; }
            set { Set(ref _icon, value); }
        }


        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public List<OrganizationModel> OrganizationChildren
        {
            get { return _organizationChildren; }
            set { Set(ref _organizationChildren, value); }
        }

        public List<PeopleModel> PeopleChildren
        {
            get { return _peopleModel; }
            set { Set(ref _peopleModel, value); }
        }

        public OrganizationModel()
        {
            OrganizationChildren = new List<OrganizationModel>();
            PeopleChildren = new List<PeopleModel>();
        }
    }
}
