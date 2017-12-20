using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using System.Reflection;

namespace ClipArtViewer
{
	// http://www.w3.org/TR/SVGTiny12/
	// http://commons.oreilly.com/wiki/index.php/SVG_Essentials
	public class ClipArtElement
	{
		public string Id { get; protected set;}
		public ClipArtElement(XmlNode node)
		{
			if (node == null)
				Id = "<null>";
			else
				Id = XmlUtil.AttrValue(node, "id");
		}
	}
	public class SVG
	{
		PaintServerManager m_paintServers = new PaintServerManager();
		Dictionary<string, Shape> m_shapes = new Dictionary<string,Shape>();
		public string Filename {get; private set;}
		public void AddShape(string id, Shape shape)
		{
			System.Diagnostics.Debug.Assert(id.Length > 0 && m_shapes.ContainsKey(id) == false);
			m_shapes[id]  = shape;
		}
		public Shape GetShape(string id)
		{
			Shape shape = null;
			m_shapes.TryGetValue(id, out shape);
			return shape;
		}
		public PaintServerManager PaintServers 
		{ 
			get { return m_paintServers; }
		}
		List<Shape> m_elements = new List<Shape>();
		public SVG()
		{
		}
		public SVG(string filename)
		{
			Filename = filename;
			XmlDocument doc = new XmlDocument();
			doc.XmlResolver = null;
			doc.Load(filename);
			XmlNode n = doc.GetElementsByTagName("svg")[0];
			this.Parse(n);
		}
		public IList<Shape> Elements
		{
			get { return m_elements.AsReadOnly(); }
		}
		void Parse(XmlNode node)
		{
			if (node == null || node.Name != "svg")
				throw new FormatException("Not a valide SVG node");
			foreach (XmlNode childnode in node.ChildNodes)
				Group.AddToList(this, m_elements, childnode, null);
		}
	}
	public class SVGRender
	{
		SVG m_svg;
		public SVG SVG
		{
			get { return m_svg;}
		}
		public DrawingGroup LoadDrawing(string filename)
		{
			m_svg = new SVG(filename);
			return CreateDrawing(m_svg);
		}
		public DrawingGroup CreateDrawing(SVG svg)
		{
			return LoadGroup(svg.Elements);
		}
		public DrawingGroup CreateDrawing(Shape shape)
		{
			return LoadGroup(new Shape[] {shape});
		}
		GeometryDrawing NewDrawingItem(ClipArtViewer.Shape shape, Geometry geometry)
		{
			GeometryDrawing item = new GeometryDrawing();
			Stroke stroke = shape.Stroke;
			if (stroke != null)
			{
				item.Pen = new Pen(stroke.StrokeBrush(SVG), stroke.Width);
				if (stroke.StrokeArray != null)
				{
					item.Pen.DashCap = PenLineCap.Flat;
					DashStyle ds = new DashStyle();
					double scale = 1 / stroke.Width;
					foreach (int dash in stroke.StrokeArray)
						ds.Dashes.Add(dash * scale);
					item.Pen.DashStyle = ds;
				}
				switch (stroke.LineCap)
				{
					case Stroke.eLineCap.butt:
						item.Pen.StartLineCap = PenLineCap.Flat;
						item.Pen.EndLineCap = PenLineCap.Flat;
						break;
					case Stroke.eLineCap.round:
						item.Pen.StartLineCap = PenLineCap.Round;
						item.Pen.EndLineCap = PenLineCap.Round;
						break;
					case Stroke.eLineCap.square:
						item.Pen.StartLineCap = PenLineCap.Square;
						item.Pen.EndLineCap = PenLineCap.Square;
						break;
				}
				switch (stroke.LineJoin)
				{
					case Stroke.eLineJoin.round:
						item.Pen.LineJoin = PenLineJoin.Round;
						break;
					case Stroke.eLineJoin.miter:
						item.Pen.LineJoin = PenLineJoin.Miter;
						break;
					case Stroke.eLineJoin.bevel:
						item.Pen.LineJoin = PenLineJoin.Bevel;
						break;
				}
			}
			if (shape.Fill != null)
			{
				item.Brush = shape.Fill.FillBrush(SVG);
				GeometryGroup g = new GeometryGroup();
				g.FillRule = FillRule.Nonzero;
				if (shape.Fill.FillRule == Fill.eFillRule.evenodd)
					g.FillRule = FillRule.EvenOdd;
				g.Children.Add(geometry);
				geometry = g;
			}
			if (shape.Transform != null)
				geometry.Transform = shape.Transform;

			// for debugging, if neither stroke or fill is set then set default pen
			if (shape.Fill == null && shape.Stroke == null)
				item.Pen = new Pen(Brushes.Blue, 1);

			item.Geometry = geometry;
			return item;
		}

		class ControlLine
		{
			public Point Ctrl {get; private set;}
			public Point Start {get; private set;}

			public ControlLine(Point start, Point ctrl)
			{
				Start = start;
				Ctrl = ctrl;
			}
			public GeometryDrawing Draw()
			{
				double size = 0.2;
				GeometryDrawing item = new GeometryDrawing();
				item.Brush = Brushes.Red;
				GeometryGroup g = new GeometryGroup();

				item.Pen = new Pen(Brushes.LightGray, size/2);
				g.Children.Add(new LineGeometry(Start, Ctrl));

				g.Children.Add(new RectangleGeometry(new Rect(Start.X-size/2, Start.Y-size/2, size, size)));
				g.Children.Add(new EllipseGeometry(Ctrl, size, size));

				item.Geometry = g;
				return item;
			}
		}

		DrawingGroup LoadGroup(IList<Shape> elements)
		{
			List<ControlLine> debugPoints = new List<ControlLine>();
			DrawingGroup grp = new DrawingGroup();
			foreach (ClipArtViewer.Shape shape in elements)
			{
				if (shape is ClipArtViewer.UseShape)
				{
					ClipArtViewer.UseShape useshape = shape as ClipArtViewer.UseShape;
					ClipArtViewer.Group group = SVG.GetShape(useshape.hRef) as ClipArtViewer.Group;
					if (group != null)
					{
						Shape oldparent = group.Parent;
						group.Parent = useshape; // this to get proper style propagated
						DrawingGroup subgroup = LoadGroup(group.Elements);
						subgroup.Transform = new TranslateTransform(useshape.X, useshape.Y);
						grp.Children.Add(subgroup);
						group.Parent = oldparent;
					}
					continue;

				}
				if (shape is ClipArtViewer.Group)
				{
					DrawingGroup subgroup = LoadGroup((shape as ClipArtViewer.Group).Elements);
					if (shape.Transform != null)
						subgroup.Transform = shape.Transform;
					grp.Children.Add(subgroup);
					continue;
				}
				if (shape is ClipArtViewer.RectangleShape)
				{
					ClipArtViewer.RectangleShape r = shape as ClipArtViewer.RectangleShape;
					RectangleGeometry rect = new RectangleGeometry(new Rect(r.X, r.Y, r.Width, r.Height));
					rect.RadiusX = r.RX;
					rect.RadiusY = r.RY;
					if (rect.RadiusX == 0 && rect.RadiusY > 0)
						rect.RadiusX = rect.RadiusY;
					grp.Children.Add(NewDrawingItem(shape, rect));
				}
				if (shape is ClipArtViewer.LineShape)
				{
					ClipArtViewer.LineShape r = shape as ClipArtViewer.LineShape;
					LineGeometry line = new LineGeometry(r.P1, r.P2);
					grp.Children.Add(NewDrawingItem(shape, line));
				}
				if (shape is ClipArtViewer.PolylineShape)
				{
					ClipArtViewer.PolylineShape r = shape as ClipArtViewer.PolylineShape;
					PathGeometry path = new PathGeometry();
					PathFigure p = new PathFigure();
					path.Figures.Add(p);
					p.IsClosed = false;
					p.StartPoint = r.Points[0];
					for (int index = 1; index < r.Points.Length; index++)
					{
						p.Segments.Add(new LineSegment(r.Points[index], true));
					}
					grp.Children.Add(NewDrawingItem(shape, path));
				}
				if (shape is ClipArtViewer.PolygonShape)
				{
					ClipArtViewer.PolygonShape r = shape as ClipArtViewer.PolygonShape;
					PathGeometry path = new PathGeometry();
					PathFigure p = new PathFigure();
					path.Figures.Add(p);
					p.IsClosed = true;
					p.StartPoint = r.Points[0];
					for (int index = 1; index < r.Points.Length; index++)
					{
						p.Segments.Add(new LineSegment(r.Points[index], true));
					}
					grp.Children.Add(NewDrawingItem(shape, path));
				}
				if (shape is ClipArtViewer.CircleShape)
				{
					ClipArtViewer.CircleShape r = shape as ClipArtViewer.CircleShape;
					EllipseGeometry c = new EllipseGeometry(new Point(r.CX, r.CY), r.R, r.R);
					grp.Children.Add(NewDrawingItem(shape, c));
				}
				if (shape is ClipArtViewer.EllipseShape)
				{
					ClipArtViewer.EllipseShape r = shape as ClipArtViewer.EllipseShape;
					EllipseGeometry c = new EllipseGeometry(new Point(r.CX, r.CY), r.RX, r.RY);
					grp.Children.Add(NewDrawingItem(shape, c));
				}
				if (shape is ClipArtViewer.ImageShape)
				{
					ClipArtViewer.ImageShape image = shape as ClipArtViewer.ImageShape;
					ImageDrawing i = new ImageDrawing(image.ImageSource, new Rect(image.X, image.Y, image.Width, image.Height));
					grp.Children.Add(i);
				}
				if (shape is ClipArtViewer.TextShape)
				{
					GeometryGroup gp = TextRender.BuildTextGeometry(shape as ClipArtViewer.TextShape);
					if (gp != null)
					{
						foreach (Geometry gm in gp.Children)
						{
							TextShape.TSpan.Element tspan = TextRender.GetElement(gm);
							if (tspan != null)
								grp.Children.Add(NewDrawingItem(tspan, gm));
							else
								grp.Children.Add(NewDrawingItem(shape, gm));
						}
					}
				}
				if (shape is ClipArtViewer.PathShape)
				{
					ClipArtViewer.PathShape r = shape as ClipArtViewer.PathShape;
					PathFigure p = null;
					Point lastPoint = new Point(0,0);

					ClipArtViewer.PathShape.CurveTo lastc = null;
					Point lastcirPoint = new Point(0,0);

					PathGeometry path = new PathGeometry();
					foreach (ClipArtViewer.PathShape.PathElement element in r.Elements)
					{
						bool isRelative = element.IsRelative;
						if (element is ClipArtViewer.PathShape.MoveTo)
						{
							p = new PathFigure();
							p.IsClosed = r.ClosePath;
							if (isRelative)
								p.StartPoint = lastPoint + (Vector)((ClipArtViewer.PathShape.MoveTo)element).Point;
							else
								p.StartPoint = ((ClipArtViewer.PathShape.MoveTo)element).Point;
							lastPoint = p.StartPoint;
							path.Figures.Add(p);
							continue;
						}
						if (element is ClipArtViewer.PathShape.LineTo)
						{
							ClipArtViewer.PathShape.LineTo lineto = element as ClipArtViewer.PathShape.LineTo;
							foreach (Point point in lineto.Points)
							{
								if (isRelative)
								{
									Point newpoint = lastPoint + (Vector)point;
									lastPoint = newpoint;
									p.Segments.Add(new LineSegment(newpoint, true));
								}
								else
								{
									if (lineto.PositionType == PathShape.LineTo.eType.Point)
										lastPoint = point;
									if (lineto.PositionType == PathShape.LineTo.eType.Horizontal)
										lastPoint = new Point(point.X, lastPoint.Y);
									if (lineto.PositionType == PathShape.LineTo.eType.Vertical)
										lastPoint = new Point(lastPoint.X, point.Y);
									p.Segments.Add(new LineSegment(lastPoint, true));
								}
							}
							continue;
						}
						if (element is ClipArtViewer.PathShape.CurveTo)
						{
							ClipArtViewer.PathShape.CurveTo c = element as ClipArtViewer.PathShape.CurveTo;
							Point startPoint = lastPoint;
							BezierSegment s = new BezierSegment();
							if (isRelative)
							{
								s.Point1 = lastPoint + (Vector)c.CtrlPoint1;

								if (c.Command == 's')
								{
									// first control point is a mirrored point of last end control point
									//s.Point1 = lastPoint + new Vector(lastc.Point.X - dx, lastc.Point.Y - dy);
									//s.Point1 = new Point(lastctrlpoint.X+2, lastctrlpoint.Y+2);

									double dx = lastc.CtrlPoint2.X - lastc.Point.X;
									double dy = lastc.CtrlPoint2.Y - lastc.Point.Y;
									s.Point1 =  new Point(lastcirPoint.X - dx, lastcirPoint.Y - dy);
									//s.Point1 = lastctrlpoint;
								}

								s.Point2 = lastPoint + (Vector)c.CtrlPoint2;
								s.Point3 = lastPoint + (Vector)c.Point;
							}
							else
							{
								s.Point1 = c.CtrlPoint1;
								s.Point2 = c.CtrlPoint2;
								s.Point3 = c.Point;
							}
							lastPoint = s.Point3;
							p.Segments.Add(s);

							lastc = c;
							lastcirPoint = s.Point3;
							
							//debugPoints.Add(new ControlLine(startPoint, s.Point1));
							//debugPoints.Add(new ControlLine(s.Point3, s.Point2));
							continue;
						}
						if (element is ClipArtViewer.PathShape.EllipticalArcTo)
						{
							ClipArtViewer.PathShape.EllipticalArcTo c = element as ClipArtViewer.PathShape.EllipticalArcTo;
							ArcSegment s = new ArcSegment();
							if (isRelative)
								s.Point = lastPoint + new Vector(c.X, c.Y);
							else
								s.Point = new Point(c.X, c.Y);

							s.Size = new Size(c.RX, c.RY);
							s.RotationAngle = c.AxisRotation;
							s.SweepDirection = SweepDirection.Counterclockwise;
							if (c.Clockwise)
								s.SweepDirection = SweepDirection.Clockwise;
							s.IsLargeArc = c.LargeArc;
							lastPoint = s.Point;
							p.Segments.Add(s);
							continue;
						}
					}
									/*
					if (r.Transform != null)
						path.Transform = r.Transform;
									  */
					grp.Children.Add(NewDrawingItem(shape, path));
				//}
				}
			}


			if (debugPoints != null)
			{
				foreach (ControlLine line in debugPoints)
					grp.Children.Add(line.Draw());
			}
			return grp;
		}
	}

}
