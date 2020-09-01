using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfLabs.Annotations;

namespace WpfLabs.EditableDataGridDemo
{
    /// <summary>
    /// EditableDataGridDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditableDataGridDemoWindow : Window, INotifyPropertyChanged
    {
        private List<DataGridModel> _dataGridModels;

        public List<DataGridModel> DataGridModels
        {
            get => _dataGridModels;
            set
            {
                _dataGridModels = value;
                OnPropertyChanged();
            }
        }

        public EditableDataGridDemoWindow()
        {
            InitializeComponent();

            DataContext = this;

            _dataGridModels = new List<DataGridModel>();
            for (int i = 0; i < 10; i++)
            {
                _dataGridModels.Add(new DataGridModel
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString()
                });
            }

            OnPropertyChanged(nameof(DataGridModels));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
