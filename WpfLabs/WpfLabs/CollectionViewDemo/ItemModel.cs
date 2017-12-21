using System;
using GalaSoft.MvvmLight;

namespace WpfLabs.CollectionViewDemo
{
    public class ItemModel : ObservableObject
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _col1;

        public string Col1
        {
            get { return _col1; }
            set { Set(ref _col1, value); }
        }

        private string _col2;

        public string Col2
        {
            get { return _col2; }
            set { Set(ref _col2, value); }
        }

        private string _col3;

        public string Col3
        {
            get { return _col3; }
            set { Set(ref _col3, value); }
        }

        private string _col4;

        public string Col4
        {
            get { return _col4; }
            set { Set(ref _col4, value); }
        }

        private string _col5;

        public string Col5
        {
            get { return _col5; }
            set { Set(ref _col5, value); }
        }

        private string _col6;

        public string Col6
        {
            get { return _col6; }
            set { Set(ref _col6, value); }
        }

        public static ItemModel GetNewItem()
        {
            return new ItemModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Col1 = Guid.NewGuid().ToString(),
                Col2 = Guid.NewGuid().ToString(),
                Col3 = Guid.NewGuid().ToString(),
                Col4 = Guid.NewGuid().ToString(),
                Col5 = Guid.NewGuid().ToString(),
                Col6 = Guid.NewGuid().ToString()
            };
        }
    }
}
