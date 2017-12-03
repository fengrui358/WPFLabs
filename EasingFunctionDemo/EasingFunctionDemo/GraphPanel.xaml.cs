using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EasingFunctionDemo.EasingFunctionConfigs;

namespace EasingFunctionDemo
{
    /// <summary>
    /// GraphPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GraphPanel
    {
        private const double XCoordinateCoefficient = 1d;
        private const double YCoordinateCoefficient = 2 / 3d;
        private const int TimePrecision = 5;
        private const int TotalMilliSeconds = 4 * 1000;
        private readonly int[] _milliSecondsUnit = new int[TotalMilliSeconds / TimePrecision];

        private readonly Point _startPoint = new Point(20,
            (240 * YCoordinateCoefficient) + (300 - 240 * YCoordinateCoefficient) / 2);

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            "EasingFunction", typeof(EasingFunctionConfig), typeof(GraphPanel),
            new FrameworkPropertyMetadata(EasingFunctionPropertyChanged));

        public EasingFunctionConfig EasingFunction
        {
            get => (EasingFunctionConfig)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public GraphPanel()
        {
            InitializeComponent();

            for (int i = 0; i < TotalMilliSeconds / TimePrecision; i++)
            {
                _milliSecondsUnit[i] = i * TimePrecision;
            }

            var cordinate = new Path {Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 1};
            var streamGeometry = new StreamGeometry();

            var xVector = StandardByCoefficient(new Vector(240, 0));
            var yVector = StandardByCoefficient(new Vector(0, -240));
            using (var context = streamGeometry.Open())
            {
                context.BeginFigure(_startPoint, false, false);
                context.LineTo(_startPoint + xVector, true, false);
                
                context.BeginFigure(_startPoint, false, false);
                context.LineTo(_startPoint + yVector, true, false);
            }
            
            cordinate.Data = streamGeometry;
            Container.Children.Add(cordinate);
        }

        private Vector StandardByCoefficient(Vector originalVector)
        {
            return new Vector(originalVector.X * XCoordinateCoefficient, originalVector.Y * YCoordinateCoefficient);
        }

        private static void EasingFunctionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graphPanel = (GraphPanel)d;
            var oldEasingFunctionConfig = e.OldValue as EasingFunctionConfig;
            var newEasingFunctionConfig = e.NewValue as EasingFunctionConfig;

            if (oldEasingFunctionConfig != null)
            {
                oldEasingFunctionConfig.ConfigEasingFunctionChanged -= graphPanel.OnEasingFunctionConfigChanged;
            }

            if (newEasingFunctionConfig != null)
            {
                newEasingFunctionConfig.ConfigEasingFunctionChanged += graphPanel.OnEasingFunctionConfigChanged;
            }

            graphPanel.DrawingGraph();
        }

        private void OnEasingFunctionConfigChanged(object sender, EventArgs args)
        {
            DrawingGraph();
        }

        private void DrawingGraph()
        {
            if (EasingFunction?.ConfigEasingFunction != null)
            {
                var streamGeometry = new StreamGeometry();
                
                using (var context = streamGeometry.Open())
                {
                    context.BeginFigure(_startPoint, false, false);

                    var easingFunctionBase = EasingFunction.ConfigEasingFunction as EasingFunctionBase;
                    if (easingFunctionBase != null)
                    {
                        foreach (int t in _milliSecondsUnit)
                        {
                            var easeComputerValue = easingFunctionBase.Ease((double) t / TotalMilliSeconds);

                            var vector = StandardByCoefficient(new Vector((double) t / TotalMilliSeconds * 240,
                                easeComputerValue * -240));

                            context.LineTo(_startPoint + vector, true, false);
                        }
                    }
                }

                EasingFunctionGraphPath.Data = streamGeometry;
            }
        }
    }
}
