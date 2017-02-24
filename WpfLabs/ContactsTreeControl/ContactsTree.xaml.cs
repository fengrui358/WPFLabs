using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfLabs.ContactsTreeControl.Models;

namespace WpfLabs.ContactsTreeControl
{
    /// <summary>
    /// ContactsTree.xaml 的交互逻辑
    /// </summary>
    public partial class ContactsTree : UserControl
    {
        /// <summary>
        /// 当前选中的用户
        /// </summary>
        private PeopleModel _currentSelectedPeopleModel;

        /// <summary>
        /// 触发选中用户
        /// </summary>
        public event Action<PeopleModel> SelectedPeople;

        /// <summary>
        /// 触发显示详细
        /// </summary>
        public event Action<PeopleModel> ShowDetail;

        /// <summary>
        /// 触发打电话
        /// </summary>
        public event Action<PeopleModel> CallPhone; 

        public static readonly DependencyProperty ItemSourcesProperty = DependencyProperty.Register(
            "ItemsSource", typeof(List<OrganizationModel>), typeof(ContactsTree), new PropertyMetadata(default(List<OrganizationModel>)));

        /// <summary>
        /// 联系人树的数据源
        /// </summary>
        public List<OrganizationModel> ItemsSource
        {
            get { return (List<OrganizationModel>) GetValue(ItemSourcesProperty); }
            set { SetValue(ItemSourcesProperty, value); }
        }

        public ContactsTree()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选中节点事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Node_OnSelected(object sender, RoutedEventArgs e)
        {
            var people = ((FrameworkElement) sender).DataContext as PeopleModel;

            SetCurrentSelectedPeople(people);
        }

        /// <summary>
        /// 点击显示详细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDetail_OnClick(object sender, RoutedEventArgs e)
        {
            var people = ((FrameworkElement) sender).DataContext as PeopleModel;

            SetCurrentSelectedPeople(people);

            if (ShowDetail != null && people != null)
            {
                ShowDetail(people);
            }
        }

        /// <summary>
        /// 点击打电话按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallPhone_OnClick(object sender, RoutedEventArgs e)
        {
            var people = ((FrameworkElement)sender).DataContext as PeopleModel;

            SetCurrentSelectedPeople(people);

            if (CallPhone != null && people != null)
            {
                CallPhone(people);
            }
        }

        /// <summary>
        /// 设置当前选中联系人
        /// </summary>
        /// <param name="currentPeopleModel"></param>
        private void SetCurrentSelectedPeople(PeopleModel currentPeopleModel)
        {
            //判断选中的节点是否是联系人，如果是才进行后续的操作
            if (currentPeopleModel != null && _currentSelectedPeopleModel != currentPeopleModel)
            {
                _currentSelectedPeopleModel = currentPeopleModel;

                //触发事件通知
                if (SelectedPeople != null)
                {
                    SelectedPeople(_currentSelectedPeopleModel);
                }
            }
        }
    }
}
