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
        /// 上一次选中联系人
        /// </summary>
        private PeopleModel _lastSelectedPeopleModel;

        /// <summary>
        /// 当前选中的联系人
        /// </summary>
        private PeopleModel _currentSelectedPeopleModel;

        /// <summary>
        /// 上一个打电话联系人
        /// </summary>
        private PeopleModel _lastCallPhonePeopleModel;

        /// <summary>
        /// 当前打电话联系人
        /// </summary>
        private PeopleModel _currentCallPhonePeopleModel;

        /// <summary>
        /// 上一个显示详细联系人
        /// </summary>
        private PeopleModel _lastShowDetailPeopleModel;

        /// <summary>
        /// 当前显示详细联系人
        /// </summary>
        private PeopleModel _currentShowDetailPeopleModel;

        public static readonly RoutedEvent SelectedPeopleChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedPeopleChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PeopleModel>), typeof(ContactsTree));

        /// <summary>
        /// 选中人员变更
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PeopleModel> SelectedPeopleChanged
        {
            add { AddHandler(SelectedPeopleChangedEvent, value); }
            remove { RemoveHandler(SelectedPeopleChangedEvent, value); }
        }

        public static readonly RoutedEvent ShowDetailEvent =
            EventManager.RegisterRoutedEvent("ShowDetail", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PeopleModel>), typeof(ContactsTree));

        /// <summary>
        /// 触发显示详细
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PeopleModel> ShowDetail
        {
            add { AddHandler(ShowDetailEvent, value); }
            remove { RemoveHandler(ShowDetailEvent, value); }
        }

        public static readonly RoutedEvent CallPhoneEvent =
            EventManager.RegisterRoutedEvent("CallPhone", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PeopleModel>), typeof(ContactsTree));

        /// <summary>
        /// 触发打电话
        /// </summary>
        public event RoutedPropertyChangedEventHandler<PeopleModel> CallPhone
        {
            add { AddHandler(CallPhoneEvent, value); }
            remove { RemoveHandler(CallPhoneEvent, value); }
        }

        public static readonly DependencyProperty ItemSourcesProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable<OrganizationModel>), typeof(ContactsTree),
            new PropertyMetadata(default(IEnumerable<OrganizationModel>)));

        /// <summary>
        /// 联系人树的数据源
        /// </summary>
        public IEnumerable<OrganizationModel> ItemsSource
        {
            get { return (IEnumerable<OrganizationModel>)GetValue(ItemSourcesProperty); }
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
            SetCurrentShowDetail(people);
        }

        /// <summary>
        /// 点击打电话按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallPhone_OnClick(object sender, RoutedEventArgs e)
        {
            var people = ((FrameworkElement) sender).DataContext as PeopleModel;

            SetCurrentSelectedPeople(people);
            SetCurrentCallPhone(people);
        }

        /// <summary>
        /// 设置当前选中联系人并触发事件
        /// </summary>
        /// <param name="currentPeopleModel"></param>
        private void SetCurrentSelectedPeople(PeopleModel currentPeopleModel)
        {
            //判断选中的节点是否是联系人，如果是才进行后续的操作
            if (currentPeopleModel != null && _currentSelectedPeopleModel != currentPeopleModel)
            {
                _lastSelectedPeopleModel = _currentSelectedPeopleModel;
                _currentSelectedPeopleModel = currentPeopleModel;

                RoutedPropertyChangedEventArgs<PeopleModel> args =
                    new RoutedPropertyChangedEventArgs<PeopleModel>(_lastSelectedPeopleModel,
                        _currentSelectedPeopleModel);

                args.RoutedEvent = SelectedPeopleChangedEvent;
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 设置当前打电话的联系人并触发事件
        /// </summary>
        /// <param name="currentPeopleModel"></param>
        private void SetCurrentCallPhone(PeopleModel currentPeopleModel)
        {
            //判断选中的节点是否是联系人，如果是才进行后续的操作
            if (currentPeopleModel != null)
            {
                _lastCallPhonePeopleModel = _currentCallPhonePeopleModel;
                _currentCallPhonePeopleModel = currentPeopleModel;

                RoutedPropertyChangedEventArgs<PeopleModel> args =
                    new RoutedPropertyChangedEventArgs<PeopleModel>(_lastCallPhonePeopleModel,
                        _currentCallPhonePeopleModel);

                args.RoutedEvent = CallPhoneEvent;
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 设置当前显示详细的联系人并触发事件
        /// </summary>
        /// <param name="currentPeopleModel"></param>
        private void SetCurrentShowDetail(PeopleModel currentPeopleModel)
        {
            //判断选中的节点是否是联系人，如果是才进行后续的操作
            if (currentPeopleModel != null)
            {
                _lastShowDetailPeopleModel = _currentShowDetailPeopleModel;
                _currentShowDetailPeopleModel = currentPeopleModel;

                RoutedPropertyChangedEventArgs<PeopleModel> args =
                    new RoutedPropertyChangedEventArgs<PeopleModel>(_lastShowDetailPeopleModel,
                        _currentShowDetailPeopleModel);

                args.RoutedEvent = ShowDetailEvent;
                RaiseEvent(args);
            }
        }
    }
}