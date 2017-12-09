using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfLabs.FoucsDemo
{
    /// <summary>
    /// FoucsDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FoucsDemoWindow
    {
        private List<Grid> _rootGrids = new List<Grid>();

        public FoucsDemoWindow()
        {
            InitializeComponent();
        }

        private void FoucsDemoWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            SubscribeEvent((Grid) sender);
            ShowAllFocusState((Grid) sender);
        }

        private void SubscribeEvent(Grid rootGrid)
        {
            _rootGrids.Add(rootGrid);
            var rootStackPanel = (StackPanel) rootGrid.Children[0];

            var t1 = (TextBlock) rootStackPanel.Children[0];

            rootGrid.GotFocus += FocusEventHandler;
            rootGrid.GotKeyboardFocus += FocusEventHandler;
            rootGrid.LostFocus += FocusEventHandler;
            rootGrid.LostKeyboardFocus += FocusEventHandler;

            rootGrid.KeyDown += (sender, args) => { t1.Text = $"按键触发{DateTime.Now:HH:mm:ss ffff}"; };

            for (var i = 2; i < rootStackPanel.Children.Count; i++)
            {
                ((Panel) rootStackPanel.Children[i]).Children[0].GotFocus += FocusEventHandler;
                ((Panel) rootStackPanel.Children[i]).Children[0].GotKeyboardFocus += FocusEventHandler;
                ((Panel) rootStackPanel.Children[i]).Children[0].LostFocus += FocusEventHandler;
                ((Panel) rootStackPanel.Children[i]).Children[0].LostKeyboardFocus += FocusEventHandler;
            }
        }

        private void ShowAllFocusState(Grid rootGrid)
        {
            var rootStackPanel = (StackPanel) rootGrid.Children[0];
            var t = (TextBlock) rootStackPanel.Children[1];

            ShowFocusState((UIElement) rootGrid, t);

            for (var i = 2; i < rootStackPanel.Children.Count; i++)
            {
                ShowFocusState(((Panel) rootStackPanel.Children[i]).Children[0]);
            }
        }

        private void FocusEventHandler(object sender, RoutedEventArgs args)
        {
            foreach (var rootGrid in _rootGrids)
            {
                ShowAllFocusState(rootGrid);
            }
        }

        private void ShowFocusState(UIElement uiElement, TextBlock ouTextBlock = null)
        {
            if (ouTextBlock != null)
            {
                ouTextBlock.Text =
                    $"{DateTime.Now:HH:mm:ss ffff}{Environment.NewLine}" +
                    $"{nameof(UIElement.Focusable)}:{uiElement.Focusable}{Environment.NewLine}" +
                    $"{nameof(UIElement.IsFocused)}:{uiElement.IsFocused}{Environment.NewLine}" +
                    $"{nameof(UIElement.IsKeyboardFocused)}:{uiElement.IsKeyboardFocused}{Environment.NewLine}" +
                    $"{nameof(UIElement.IsKeyboardFocusWithin)}:{uiElement.IsKeyboardFocusWithin}";
            }
            else
            {
                var parent = LogicalTreeHelper.GetParent(uiElement);

                var index = 0;
                foreach (var child in LogicalTreeHelper.GetChildren(parent))
                {
                    if (index == 1 && child is TextBlock)
                    {
                        ((TextBlock) child).Text =
                            $"{DateTime.Now:HH:mm:ss ffff}-{nameof(UIElement.Focusable)}:{uiElement.Focusable}--{nameof(UIElement.IsFocused)}:{uiElement.IsFocused}--{nameof(UIElement.IsKeyboardFocused)}:{uiElement.IsKeyboardFocused}--{nameof(UIElement.IsKeyboardFocusWithin)}:{uiElement.IsKeyboardFocusWithin}";
                    }

                    ++index;
                }
            }
        }
    }
}