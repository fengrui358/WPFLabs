using GalaSoft.MvvmLight;

namespace WpfLabs.ContactsTreeControl.Models
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
