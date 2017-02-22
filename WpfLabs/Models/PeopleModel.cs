using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfLabs.Models
{
    public class PeopleModel : ObservableObject
    {
        private long _id;
        private string _name;

        private long Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
    }
}
