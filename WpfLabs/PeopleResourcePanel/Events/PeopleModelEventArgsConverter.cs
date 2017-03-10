using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using WpfLabs.Base;
using WpfLabs.PeopleResourcePanel.Models;

namespace WpfLabs.PeopleResourcePanel.Events
{
    [ValueConversion(typeof(RoutedPropertyEventArgs<PeopleModel>), typeof(PeopleModel))]
    public class PeopleModelEventArgsConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            return ((RoutedPropertyEventArgs<PeopleModel>)value).Value;
        }
    }
}
