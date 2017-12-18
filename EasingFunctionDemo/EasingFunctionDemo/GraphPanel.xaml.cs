using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private const double YCoordinateCoefficient = 1d;
        private const int TimePrecision = 10;
        private readonly int[] _milliSecondsUnit = new int[ConstData.TotalMilliSeconds / TimePrecision];
        private readonly List<double> _runningMilliSecondsUnit = new List<double>();

        private readonly Point _startPoint;

        private readonly Point _endPoint;
        private readonly Vector _endPointVector = new Vector(240, -240 * YCoordinateCoefficient);
        private Point _bezierControlPoint1;
        private Point _bezierControlPoint2;

        private Ellipse _controlPoint1;
        private Ellipse _controlPoint2;

        private Line _controlLine1;
        private Line _controlLine2;

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            "EasingFunction", typeof(EasingFunctionConfig), typeof(GraphPanel),
            new FrameworkPropertyMetadata(EasingFunctionPropertyChanged));

        public EasingFunctionConfig EasingFunction
        {
            get => (EasingFunctionConfig) GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty CurrentProgressProperty = DependencyProperty.Register(
            "CurrentProgress", typeof(double), typeof(GraphPanel),
            new FrameworkPropertyMetadata(OnCurrentProgressChanged));

        public double CurrentProgress
        {
            get => (double) GetValue(CurrentProgressProperty);
            set => SetValue(CurrentProgressProperty, value);
        }

        public GraphPanel()
        {
            InitializeComponent();

            _startPoint = new Point(20,
                (240 * YCoordinateCoefficient) + (460 - 240 * YCoordinateCoefficient) / 2);
            _endPoint = _startPoint + _endPointVector;

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
            var graphPanel = (GraphPanel) d;
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
            if (EasingFunction?.ConfigEasingFunction != null || EasingFunction?.SplineKeyFrameConfig != null)
            {
                var streamGeometry = new StreamGeometry();

                using (var context = streamGeometry.Open())
                {
                    context.BeginFigure(_startPoint, false, false);

                    if (!EasingFunction.IsSplineKeyFrame)
                    {
                        RemoveControlPoints();

                        if (EasingFunction.ConfigEasingFunction is EasingFunctionBase)
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
                        _bezierControlPoint1 =
                            _startPoint + new Vector(splineKeyFrameConfig.ControlPoint1X * _endPointVector.X,
                                splineKeyFrameConfig.ControlPoint1Y * _endPointVector.Y);
                        _bezierControlPoint2 =
                            _startPoint + new Vector(splineKeyFrameConfig.ControlPoint2X * _endPointVector.X,
                                splineKeyFrameConfig.ControlPoint2Y * _endPointVector.Y);

                        context.BezierTo(_bezierControlPoint1, _bezierControlPoint2, _endPoint, true, false);

                        //绘制控制点
                        if (_controlPoint1 == null)
                        {
                            _controlPoint1 = new Ellipse {Width = 6, Height = 6, Fill = Brushes.DarkOrange};
                            Container.Children.Add(_controlPoint1);

                            _controlLine1 = new Line {Stroke = Brushes.DarkOrange, StrokeThickness = 1};
                            Container.Children.Add(_controlLine1);
                        }

                        Canvas.SetLeft(_controlPoint1, _bezierControlPoint1.X - 3);
                        Canvas.SetTop(_controlPoint1, _bezierControlPoint1.Y - 3);
                        _controlLine1.X1 = _bezierControlPoint1.X;
                        _controlLine1.Y1 = _bezierControlPoint1.Y;
                        _controlLine1.X2 = _startPoint.X;
                        _controlLine1.Y2 = _startPoint.Y;

                        if (_controlPoint2 == null)
                        {
                            _controlPoint2 = new Ellipse {Width = 6, Height = 6, Fill = Brushes.DarkCyan};
                            Container.Children.Add(_controlPoint2);

                            _controlLine2 = new Line {Stroke = Brushes.DarkCyan, StrokeThickness = 1};
                            Container.Children.Add(_controlLine2);
                        }

                        Canvas.SetLeft(_controlPoint2, _bezierControlPoint2.X - 3);
                        Canvas.SetTop(_controlPoint2, _bezierControlPoint2.Y - 3);
                        _controlLine2.X1 = _bezierControlPoint2.X;
                        _controlLine2.Y1 = _bezierControlPoint2.Y;
                        _controlLine2.X2 = _endPoint.X;
                        _controlLine2.Y2 = _endPoint.Y;
                    }
                }

                EasingFunctionGraphPath.Data = streamGeometry;
            }
        }

        private static void OnCurrentProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graphPanel = (GraphPanel) d;

            if (graphPanel.RunningFunctionGraphPath.Data == null)
            {
                graphPanel.RunningFunctionGraphPath.Data = new StreamGeometry();
            }
            var streamGeometry = (StreamGeometry) graphPanel.RunningFunctionGraphPath.Data;
            streamGeometry.Clear();

            var newTime = (double) e.NewValue;

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

                for (var i = 0; i < runningMilliSecondsUnit.Count; i++)
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

        private Vector GetChangeVectorByTime(double milliseconds)
        {
            if (!EasingFunction.IsSplineKeyFrame)
            {
                var easingFunctionBase = EasingFunction?.ConfigEasingFunction as EasingFunctionBase;
                if (easingFunctionBase != null)
                {
                    var easeComputerValue = easingFunctionBase.Ease(milliseconds / ConstData.TotalMilliSeconds);

                    var vector = StandardByCoefficient(new Vector(milliseconds / ConstData.TotalMilliSeconds * 240,
                        easeComputerValue * -240));
                    return vector;
                }
            }
            else
            {
                var splineKeyFrameConfig = EasingFunction?.SplineKeyFrameConfig;
                if (splineKeyFrameConfig != null)
                {
                    var t = milliseconds / ConstData.TotalMilliSeconds;

                    //公式参考：https://www.cnblogs.com/hnfxs/p/3148483.html
                    var value = new Vector(_startPoint.X, _startPoint.Y) * Math.Pow(1 - t, 3) +
                                3 * new Vector(_bezierControlPoint1.X, _bezierControlPoint1.Y) * t *
                                Math.Pow(1 - t, 2) +
                                3 * new Vector(_bezierControlPoint2.X, _bezierControlPoint2.Y) * Math.Pow(t, 2) *
                                (1 - t) +
                                new Vector(_endPoint.X, _endPoint.Y) * Math.Pow(t, 3);

                    return new Vector(value.X - _startPoint.X, value.Y - _startPoint.Y);
                }
            }

            return new Vector();
        }

        /// <summary>
        /// 清理三次贝塞尔速率曲线的控制点
        /// </summary>
        private void RemoveControlPoints()
        {
            _bezierControlPoint1 = new Point(0, 0);
            _bezierControlPoint2 = new Point(0, 0);

            if (_controlPoint1 != null)
            {
                Container.Children.Remove(_controlPoint1);
            }

            if (_controlPoint2 != null)
            {
                Container.Children.Remove(_controlPoint2);
            }

            if (_controlLine1 != null)
            {
                Container.Children.Remove(_controlLine1);
            }

            if (_controlLine2 != null)
            {
                Container.Children.Remove(_controlLine2);
            }
        }
    }
}