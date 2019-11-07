using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace WpfLabs.AdornerDemo
{
    /// <summary>
    /// Interaction logic for AdornerControlWindow.xaml
    /// </summary>
    public partial class AdornerControlWindow : Window
    {
        bool isDown, isDragging, isSelected;
        UIElement selectedElement = null;
        double originalLeft, originalTop;
        Point startPoint;

        AdornerLayer adornerLayer;

        public AdornerControlWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //registering mouse events
            this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
            this.MouseLeftButtonUp += MainWindow_MouseLeftButtonUp;
            this.MouseMove += MainWindow_MouseMove;
            this.MouseLeave += MainWindow_MouseLeave;

            myCanvas.PreviewMouseLeftButtonDown += MyCanvas_PreviewMouseLeftButtonDown;
            myCanvas.PreviewMouseLeftButtonUp += MyCanvas_PreviewMouseLeftButtonUp;
        }

        private void StopDragging()
        {
            if (isDown)
            {
                isDown = isDragging = false;
            }
        }


        private void MyCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopDragging();
            e.Handled = true;
        }

        private void MyCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //removing selected element
            if (isSelected)
            {
                isSelected = false;
                if (selectedElement != null)
                {
                    adornerLayer.Remove(adornerLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }

            // select element if any element is clicked other then canvas
            if (e.Source != myCanvas)
            {
                isDown = true;
                startPoint = e.GetPosition(myCanvas);

                selectedElement = e.Source as UIElement;

                originalLeft = Canvas.GetLeft(selectedElement);
                originalTop = Canvas.GetTop(selectedElement);

                //adding adorner on selected element
                adornerLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                adornerLayer.Add(new BorderAdorner(selectedElement));
                isSelected = true;
                e.Handled = true;
            }
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //handling mouse move event and setting canvas top and left value based on mouse movement
            if (isDown)
            {
                if ((!isDragging) &&
                    ((Math.Abs(e.GetPosition(myCanvas).X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(myCanvas).Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                    isDragging = true;

                if (isDragging)
                {
                    Point position = Mouse.GetPosition(myCanvas);
                    Canvas.SetTop(selectedElement, position.Y - (startPoint.Y - originalTop));
                    Canvas.SetLeft(selectedElement, position.X - (startPoint.X - originalLeft));
                }
            }
        }

        private void MainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            //stop dragging on mouse leave
            StopDragging();
            e.Handled = true;
        }

        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //stop dragging on mouse left button up
            StopDragging();
            e.Handled = true;
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //remove selected element on mouse down
            if (isSelected)
            {
                isSelected = false;
                if (selectedElement != null)
                {
                    adornerLayer.Remove(adornerLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }
        }

        private void MyButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
