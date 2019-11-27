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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using WpfLabs.AdornerDemo;
using WpfLabs.AnimationPerformanceDemo;
using WpfLabs.BindingDemo;
using WpfLabs.CalloutBorder;
using WpfLabs.CircularLoading;
using WpfLabs.CollectionViewDemo;
using WpfLabs.ContactsTreeControl;
using WpfLabs.ContactsTreeControl.Models;
using WpfLabs.CustomWindow;
using WpfLabs.DataGridDetailList;
using WpfLabs.DrawingDemo;
using WpfLabs.EllipsisLoading;
using WpfLabs.ExpertPanel;
using WpfLabs.ExpertPanel2;
using WpfLabs.FlexDataGrid;
using WpfLabs.FluidLayoutDemo;
using WpfLabs.FontFamilyDemo;
using WpfLabs.FoucsDemo;
using WpfLabs.GifShowDemo;
using WpfLabs.GroupExpander;
using WpfLabs.ImagePerformanceDemo;
using WpfLabs.ItemsControlWithTimeline;
using WpfLabs.KeyBoardInputDemo;
using WpfLabs.MasterDetailDataGrid;
using WpfLabs.MeasureOverrideAndArrangeOverride;
using WpfLabs.MultiSceenDemo;
using WpfLabs.MusicPlayer;
using WpfLabs.MutliUiThreadingDemo;
using WpfLabs.NewCallAnimation;
using WpfLabs.NotifyMsgPresenter;
using WpfLabs.PeopleResourcePanel;
using WpfLabs.ResourceDictionaryCodeBehind;
using WpfLabs.ScreenDragDemo;
using WpfLabs.ShortCutDemo;
using WpfLabs.SwapImageDemo;
using WpfLabs.Timer;
using WpfLabs.TreeViewDemo;
using WpfLabs.WaterMarkDemo;

namespace WpfLabs
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private readonly List<ContentControl> _items = new List<ContentControl>();
        public RelayCommand<string> ShowControlWindow { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            ShowControlWindow = new RelayCommand<string>(OnShowControlWindow);
        }

        private void OnShowControlWindow(string controlName)
        {
            this.Effect = new BlurEffect();

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
                case "DrawingDemoWindow":
                    var drawingDemoWindow = new DrawingDemoWindow();
                    drawingDemoWindow.ShowDialog();
                    break; 
                case "WaterMarkDemoWindow":
                    var waterMarkDemoWindow = new WaterMarkDemoWindow();
                    waterMarkDemoWindow.ShowDialog();
                    break;
                case "MultiUiThreadingDemoWindow":
                    var mutliUiThreadingDemoWindow = new MultiUiThreadingDemoWindow();
                    mutliUiThreadingDemoWindow.ShowDialog();
                    break;
                case "CustomPixelShaderDemo":
                    var customPixelShaderDemo = new CustomPixelShaderDemo.CustomPixelShaderDemo();
                    customPixelShaderDemo.ShowDialog();
                    break;
                case "GifShowDemoWindow":
                    var gifShowDemoWindow = new GifShowDemoWindow();
                    gifShowDemoWindow.ShowDialog();
                    break;
                case "NewCallAnimationWindow":
                    var newCallAnimationWindow = new NewCallAnimationWindow();
                    newCallAnimationWindow.ShowDialog();
                    break;
                case "GenerateBitmapDemo":
                    var generateBitmapDemo = new GenerateBitmapDemo.GenerateBitmapDemo();
                    generateBitmapDemo.ShowDialog();
                    break;
                case "FontFamilyDemoWindow":
                    var fontFamilyDemoWindow = new FontFamilyDemoWindow();
                    fontFamilyDemoWindow.ShowDialog();
                    break;
                case "FoucsDemoWindow":
                    var foucsDemoWindow = new FoucsDemoWindow();
                    foucsDemoWindow.ShowDialog();
                    break;
                case "KeyBoardInputWindow":
                    var keyBoardInputWindow = new KeyBoardInputWindow();
                    keyBoardInputWindow.ShowDialog();
                    break;
                case "EllipsisLoadingDemoWindow":
                    var ellipsisLoadingDemoWindow = new EllipsisLoadingDemoWindow();
                    ellipsisLoadingDemoWindow.ShowDialog();
                    break;
                case "ImagePerformanceWindow":
                    var imagePerformanceWindow = new ImagePerformanceWindow();
                    imagePerformanceWindow.ShowDialog();
                    break;
                case "CollectionViewWindow":
                    var collectionViewWindow = new CollectionViewWindow();
                    collectionViewWindow.ShowDialog();
                    break;
                case "MultiSceenDemoWindow":
                    var multiSceenDemoWindow = new MultiSceenDemoWindow();
                    multiSceenDemoWindow.ShowDialog();
                    break;
                case "ResourceDictionaryCodeBehindWindow":
                    var resourceDictionaryCodeBehindWindow = new ResourceDictionaryCodeBehindWindow();
                    resourceDictionaryCodeBehindWindow.ShowDialog();
                    break;
                case "TreeViewDemoWindow":
                    var treeViewDemoWindow = new TreeViewDemoWindow();
                    treeViewDemoWindow.ShowDialog();
                    break;
                case "GroupExpanderWindow":
                    var groupExpanderWindow = new GroupExpanderWindow();
                    groupExpanderWindow.ShowDialog();
                    break;
                case "ShortCutDemoWindow":
                    var shortCutDemoWindow = new ShortCutDemoWindow();
                    shortCutDemoWindow.ShowDialog();
                    break;
                case "ScreenDragMainWindow":
                    var screenDragMainWindow = new ScreenDragMainWindow();
                    screenDragMainWindow.ShowDialog();
                    break;
                case "AnimationPerformanceWindow":
                    var animationPerformanceWindow = new AnimationPerformanceWindow();
                    animationPerformanceWindow.ShowDialog();
                    break;
                case "FluidLayoutDemoWindow":
                    var fluidLayoutDemoWindow = new FluidLayoutDemoWindow();
                    fluidLayoutDemoWindow.ShowDialog();
                    break;
                case "DragableTabControlDemo":
                    var dragableTabControlDemo = new DragableTabControlDemo.DragableTabControlDemo();
                    dragableTabControlDemo.ShowDialog();
                    break;
                case "DragableListBoxDemo":
                    var dragableListBoxDemo = new DragableListBoxDemo.DragableListBoxDemo();
                    dragableListBoxDemo.ShowDialog();
                    break;
                case "CustomWindowDemo":
                    var customWindowDemo = new CustomWindowDemo();
                    customWindowDemo.ShowDialog();
                    break;
                case "AdornerControlWindow":
                    var adornerControlWindow = new AdornerControlWindow();
                    adornerControlWindow.ShowDialog();
                    break;
                case "ItemsControlWithTimelineWindow":
                    var itemsControlWithTimelineWindow = new ItemsControlWithTimelineWindow();
                    itemsControlWithTimelineWindow.ShowDialog();
                    break;
            }

            this.Effect = null;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = (TextBox) sender;
            List<ContentControl> items;

            if (!string.IsNullOrEmpty(txtBox.Text))
            {
                items = _items.Where(s => s.Content.ToString().ToUpper().Contains(txtBox.Text.ToUpper())).ToList();
            }
            else
            {
                items = _items;
            }

            Container.Items.Clear();
            foreach (var contentControl in items)
            {
                Container.Items.Add(contentControl);
            }
        }

        private void Container_OnLoaded(object sender, RoutedEventArgs e)
        {
            var itemsControl = (ItemsControl) sender;
            foreach (var itemsControlItem in itemsControl.Items)
            {
                _items.Add((ContentControl) itemsControlItem);
            }
        }
    }
}
