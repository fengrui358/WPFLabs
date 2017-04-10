using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfLabs.ExpertPanel.Models
{
    public class ExpertModel : ObservableObject
    {
        private long _id;
        private string _portrait;
        private string _name;
        private string _title;
        private string _phoneNumber;

        public long Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        public string Portrait
        {
            get { return _portrait; }
            set { Set(ref _portrait, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { Set(ref _phoneNumber, value); }
        }
    }
}
