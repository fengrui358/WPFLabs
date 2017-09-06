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
using WpfLabs.BindingDemo;
using WpfLabs.CalloutBorder;
using WpfLabs.CircularLoading;
using WpfLabs.ContactsTreeControl;
using WpfLabs.ContactsTreeControl.Models;
using WpfLabs.DataGridDetailList;
using WpfLabs.ExpertPanel;
using WpfLabs.ExpertPanel2;
using WpfLabs.FlexDataGrid;
using WpfLabs.MasterDetailDataGrid;
using WpfLabs.MeasureOverrideAndArrangeOverride;
using WpfLabs.MusicPlayer;
using WpfLabs.NotifyMsgPresenter;
using WpfLabs.PeopleResourcePanel;
using WpfLabs.SwapImageDemo;
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
                case "ExpertPanel2":
                    var expertPanelWindow2 = new ExpertPanelWindow2();
                    expertPanelWindow2.ShowDialog();
                    break;
                case "Timer":
                    var timerWindow = new TimerWindow();
                    timerWindow.ShowDialog();
                    break;
                case "MusicPlayer":
                    var musicPlayerWindow = new MusicPlayerWindow();
                    musicPlayerWindow.ShowDialog();
                    break;
                case "PeopleResourcePanel":
                    var peopleResourcePanelWindow = new PeopleResourcePanelWindow();
                    peopleResourcePanelWindow.ShowDialog();
                    break;
                case "NotifyMsgPresenter":
                    var notifyMsgPresenterWindow = new NotifyMsgPresenterWindow();
                    notifyMsgPresenterWindow.ShowDialog();
                    break;
                case "MasterDetailDataGrid":
                    var masterDetailDataGridWindow = new MasterDetailDataGridWindow();
                    masterDetailDataGridWindow.ShowDialog();
                    break;
                case "FlexDataGrid":
                    var flexDataGridWindow = new FlexDataGridWindow();
                    flexDataGridWindow.ShowDialog();
                    break;
                case "DataGridDetailListWindow":
                    var dataGridDetailListWindow = new DataGridDetailListWindow();
                    dataGridDetailListWindow.ShowDialog();
                    break;
                case "BindingDemoWindow":
                    var bindingDemoWindow = new BindingDemoWindow();
                    bindingDemoWindow.ShowDialog();
                    break;
                case "SwapImageDemoWindow":
                    var swapImageDemoWindow = new SwapImageDemoWindow();
                    swapImageDemoWindow.ShowDialog();
                    break;
                case "CircularLoadingWindow":
                    var circularLoadingWindow = new CircularLoadingWindow();
                    circularLoadingWindow.ShowDialog();
                    break;
                case "CalloutBorderStyleWindow":
                    var calloutBorderStyleWindow = new CalloutBorderStyleWindow();
                    calloutBorderStyleWindow.ShowDialog();
                    break;
                case "MeasureOverrideAndArrangeOverrideWindow":
                    var measureOverrideAndArrangeOverrideWindow = new MeasureOverrideAndArrangeOverrideWindow();
                    measureOverrideAndArrangeOverrideWindow.ShowDialog();
                    break;
            }
        }
    }
}
