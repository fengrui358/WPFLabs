using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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
        private readonly int[] _milliSecondsUnit = new int[ConstData.TotalMilliSeconds / TimePrecision];
        private readonly List<double> _runningMilliSecondsUnit = new List<double>();

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

        public static readonly DependencyProperty CurrentProgressProperty = DependencyProperty.Register(
            "CurrentProgress", typeof(double), typeof(GraphPanel), new FrameworkPropertyMetadata(OnCurrentProgressChanged));

        public double CurrentProgress
        {
            get => (double)GetValue(CurrentProgressProperty);
            set => SetValue(CurrentProgressProperty, value);
        }

        public GraphPanel()
        {
            InitializeComponent();

            for (int i = 0; i < ConstData.TotalMilliSeconds / TimePrecision; i++)
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

                    if (!EasingFunction.IsSplineKeyFrame)
                    {
                        var easingFunctionBase = EasingFunction.ConfigEasingFunction as EasingFunctionBase;
                        if (easingFunctionBase != null)
                        {
                            foreach (var t in _milliSecondsUnit)
                            {
                                var vector = GetChangeVectorByTime(t);
                                context.LineTo(_startPoint + vector, true, false);
                            }
                        }
                    }
                    else
                    {
                        var splineKeyFrameConfig = EasingFunction.SplineKeyFrameConfig;

                    }
                }

                EasingFunctionGraphPath.Data = streamGeometry;
            }
        }

        private static void OnCurrentProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graphPanel = (GraphPanel)d;

            var easingFunctionBase = graphPanel.EasingFunction?.ConfigEasingFunction as EasingFunctionBase;
            if (easingFunctionBase != null)
            {
                if (graphPanel.RunningFunctionGraphPath.Data == null)
                {
                    graphPanel.RunningFunctionGraphPath.Data = new StreamGeometry();
                }
                var streamGeometry = (StreamGeometry) graphPanel.RunningFunctionGraphPath.Data;
                streamGeometry.Clear();

                var newTime = (double)e.NewValue;

                using (var context = streamGeometry.Open())
                {
                    var runningMilliSecondsUnit = graphPanel._runningMilliSecondsUnit;
                    if (runningMilliSecondsUnit.Any())
                    {
                        var lastTime = runningMilliSecondsUnit.Last();                           

                        if (newTime < lastTime)
                        {
                            runningMilliSecondsUnit.Clear();
                        }
                    }

                    runningMilliSecondsUnit.Add(newTime);

                    context.BeginFigure(graphPanel._startPoint, false, false);

                    for (int i = 0; i < runningMilliSecondsUnit.Count; i++)
                    {
                        var vector = graphPanel.GetChangeVectorByTime(runningMilliSecondsUnit[i]);

                        var currentPoint = graphPanel._startPoint + vector;
                        if (i + 1 == runningMilliSecondsUnit.Count)
                        {
                            graphPanel.TimeLabel.Text = newTime.ToString("F2");
                            graphPanel.ValueLabel.Text = (-vector.Y / YCoordinateCoefficient).ToString("F2");
                        }

                        context.LineTo(currentPoint, true, false);
                    }
                }
            }
        }

        private Vector GetChangeVectorByTime(double milliseconds)
        {
            var easingFunctionBase = EasingFunction?.ConfigEasingFunction as EasingFunctionBase;
            if (easingFunctionBase != null)
            {
                var easeComputerValue = easingFunctionBase.Ease(milliseconds / ConstData.TotalMilliSeconds);

                var vector = StandardByCoefficient(new Vector(milliseconds / ConstData.TotalMilliSeconds * 240,
                    easeComputerValue * -240));
                return vector;
            }

            return new Vector();
        }
    }
}
