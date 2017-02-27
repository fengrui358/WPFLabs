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
using GalaSoft.MvvmLight.Command;
using WpfLabs.ContactsTreeControl;
using WpfLabs.ContactsTreeControl.Models;
using WpfLabs.ExpertPanel;
using WpfLabs.MusicPlayer;
using WpfLabs.Timer;

namespace WpfLabs
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public RelayCommand<string> ShowControlWindow { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            ShowControlWindow = new RelayCommand<string>(OnShowControlWindow);
        }

        private void OnShowControlWindow(string controlName)
        {
            switch (controlName)
            {
                case "ContactsTree":
                    var contactsTreeWindow = new ContactsTreeWindow();
                    contactsTreeWindow.ShowDialog();
                    break;
                case "ExpertPanel":
                    var expertPanelWindow = new ExpertPanelWindow();
                    expertPanelWindow.ShowDialog();
                    break;
                case "Timer":
                    var timerWindow = new TimerWindow();
                    timerWindow.ShowDialog();
                    break;
                case "MusicPlayer":
                    var musicPlayerWindow = new MusicPlayerWindow();
                    musicPlayerWindow.ShowDialog();
                    break;
            }
        }
    }
}
