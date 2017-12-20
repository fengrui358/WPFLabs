using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClipArtViewer
{
	public class Shape : ClipArtElement
	{
		Fill m_fill;
		Stroke m_stroke;
		TextStyle m_textstyle;
		public virtual Stroke Stroke 
		{ 
			get
			{
				if (m_stroke != null)
					return m_stroke;
				while (Parent != null)
				{
					if (Parent.Fill != null)
						return Parent.m_stroke;
					Parent = Parent.Parent;
				}
				return null;
			}
		}
		public virtual Fill Fill 
		{ 
			get 
			{
				if (m_fill != null)
					return m_fill;
				while (Parent != null)
				{
					if (Parent.Fill != null)
						return Parent.Fill;
					Parent = Parent.Parent;
				}
				return null;
			}
		}
		public virtual TextStyle TextStyle
		{
			get 
			{
				if (m_textstyle != null)
					return m_textstyle;
				while (Parent != null)
				{
					if (Parent.m_textstyle != null)
						return Parent.m_textstyle;
					Parent = Parent.Parent;
				}
				return null;
			}
		}
		public virtual Transform Transform { get; private set;}
		public Shape Parent { get; set; }
		public Shape(SVG svg, XmlNode node) : this(svg, node, null) {}
		public Shape(SVG svg, XmlNode node, Shape parent) : base(node)
		{
			Parent = parent;
			if (node != null)
			{
				foreach (XmlAttribute attr in node.Attributes)
					Parse(svg, attr);
			}
		}
		public Shape(SVG svg, List<ShapeUtil.Attribute> attrs, Shape parent) : base(null)
		{
			Parent = parent;
			if (attrs != null)
			{
				foreach (ShapeUtil.Attribute attr in attrs)
					Parse(svg, attr);
			}
		}
		protected virtual void Parse(SVG svg, XmlAttribute attr)
		{
			string name = attr.Name;
			string value = attr.Value;
			Parse(svg, name, value);
		}
		protected virtual void Parse(SVG svg, ShapeUtil.Attribute attr)
		{
			string name = attr.Name;
			string value = attr.Value;
			Parse(svg, name, value);
		}
		protected virtual void Parse(SVG svg, string name, string value)
		{
			if (name == SVGTags.sTransform)
			{
				Transform = ShapeUtil.ParseTransform(value.ToLower());
				return;
			}
			if (name == SVGTags.sStroke)
			{
				GetStroke(svg).Color = svg.PaintServers.Parse(value);
				return;
			}
			if (name == SVGTags.sStrokeWidth)
			{
				GetStroke(svg).Width = XmlUtil.ParseDouble(svg, value);
				return;
			}
			if (name == SVGTags.sStrokeOpacity)
			{
				GetStroke(svg).Opacity = XmlUtil.ParseDouble(svg, value) * 100;
				return;
			}
			if (name == SVGTags.sStrokeDashArray)
			{
				if (value == "none")
				{
					GetStroke(svg).StrokeArray = null;
					return;
				}
				ShapeUtil.StringSplitter sp = new ShapeUtil.StringSplitter(value);
				List<double> a = new List<double>();
				while(sp.More)
				{
					a.Add(sp.ReadNextValue());
				}
				GetStroke(svg).StrokeArray = a.ToArray();
				return;
			}
			if (name == SVGTags.sStrokeLinecap)
			{
				GetStroke(svg).LineCap = (ClipArtViewer.Stroke.eLineCap)Enum.Parse(typeof(ClipArtViewer.Stroke.eLineCap), value);
				return;
			}
			if (name == SVGTags.sStrokeLinejoin)
			{
				GetStroke(svg).LineJoin= (ClipArtViewer.Stroke.eLineJoin)Enum.Parse(typeof(ClipArtViewer.Stroke.eLineJoin), value);
				return;
			}
			if (name == SVGTags.sFill)
			{
				GetFill(svg).Color = svg.PaintServers.Parse(value);
				return;
			}
			if (name == SVGTags.sFillOpacity)
			{
				GetFill(svg).Opacity = XmlUtil.ParseDouble(svg, value) * 100;
				return;
			}
			if (name == SVGTags.sFillRule)
			{
				GetFill(svg).FillRule = (Fill.eFillRule)Enum.Parse(typeof(Fill.eFillRule), value);
				return;
			}
			if (name == SVGTags.sStyle)
			{
				foreach (ShapeUtil.Attribute item in XmlUtil.SplitStyle(svg, value))
					Parse(svg, item);
			}
			//********************** text *******************
			if (name == SVGTags.sFontFamily)
			{
				GetTextStyle(svg).FontFamily = value;
				return;
			}
			if (name == SVGTags.sFontSize)
			{
				GetTextStyle(svg).FontSize = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
				return;
			}
			if (name == SVGTags.sFontWeight)
			{
				GetTextStyle(svg).Fontweight = (FontWeight)new FontWeightConverter().ConvertFromString(value);
				return;
			}
			if (name == SVGTags.sFontStyle)
			{
				GetTextStyle(svg).Fontstyle = (FontStyle)new FontStyleConverter().ConvertFromString(value);
				return;
			}
			if (name == SVGTags.sTextDecoration)
			{
				TextDecoration t = new TextDecoration();
				if (value == "none")
					return;
				if (value == "underline")
					t.Location = TextDecorationLocation.Underline;
				if (value == "overline")
					t.Location = TextDecorationLocation.OverLine;
				if (value == "line-through")
					t.Location = TextDecorationLocation.Strikethrough;
				TextDecorationCollection tt = new TextDecorationCollection();
				tt.Add(t);
				GetTextStyle(svg).TextDecoration = tt;
				return;
			}
			if (name == SVGTags.sTextAnchor)
			{
				if (value == "start")
					GetTextStyle(svg).TextAlignment = TextAlignment.Left;
				if (value == "middle")
					GetTextStyle(svg).TextAlignment = TextAlignment.Center;
				if (value == "end")
					GetTextStyle(svg).TextAlignment = TextAlignment.Right;
				return;
			}
			if(name == "word-spacing")
			{
				GetTextStyle(svg).WordSpacing = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
				return;
			}
			if(name == "letter-spacing")
			{
				GetTextStyle(svg).LetterSpacing = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
				return;
			}
			if (name == "baseline-shift")
			{
				//GetTextStyle(svg).BaseLineShift = XmlUtil.AttrValue(new ShapeUtil.Attribute(name, value));
				GetTextStyle(svg).BaseLineShift = value;
				return;
			}
		}
		Stroke GetStroke(SVG svg)
		{
			if (m_stroke == null)
				m_stroke = new Stroke(svg);
			return m_stroke;
		}
		protected Fill GetFill(SVG svg)
		{
			if (m_fill == null)
				m_fill = new Fill(svg);
			return m_fill;
		}
		protected TextStyle GetTextStyle(SVG svg)
		{
			if (m_textstyle == null)
				m_textstyle = new TextStyle(svg, this);
			return m_textstyle;
		}
	}
	class RectangleShape : Shape
	{
		static Fill DefaultFill = null;
		public override Fill Fill 
		{ 
			get 
			{
				Fill f = base.Fill;
				if (f == null)
					f = DefaultFill;
				return f;
			}
		}
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public double RX { get; set; }
		public double RY { get; set; }
		public RectangleShape(SVG svg, XmlNode node) : base(svg, node)
		{
			X = XmlUtil.AttrValue(node, "x", 0);
			Y = XmlUtil.AttrValue(node, "y", 0);
			Width = XmlUtil.AttrValue(node, "width", 0);
			Height = XmlUtil.AttrValue(node, "height", 0);
			RX = XmlUtil.AttrValue(node, "rx", 0);
			RY = XmlUtil.AttrValue(node, "ry", 0);

			if (DefaultFill == null)
			{
				DefaultFill = new Fill(svg);
				DefaultFill.Color = svg.PaintServers.Parse("black");
			}
		}
	}
	class CircleShape : Shape
	{
		public double CX { get; set; }
		public double CY { get; set; }
		public double R { get; set; }
		public CircleShape(SVG svg, XmlNode node) : base(svg, node)
		{
			CX = XmlUtil.AttrValue(node, "cx", 0);
			CY = XmlUtil.AttrValue(node, "cy", 0);
			R = XmlUtil.AttrValue(node, "r", 0);
		}
	}
	class EllipseShape : Shape
	{
		public double CX { get; set; }
		public double CY { get; set; }
		public double RX { get; set; }
		public double RY { get; set; }
		public EllipseShape(SVG svg, XmlNode node) : base(svg, node)
		{
			CX = XmlUtil.AttrValue(node, "cx", 0);
			CY = XmlUtil.AttrValue(node, "cy", 0);
			RX = XmlUtil.AttrValue(node, "rx", 0);
			RY = XmlUtil.AttrValue(node, "ry", 0);
		}
	}
	class LineShape : Shape
	{
		public Point P1 {get; private set;}
		public Point P2 {get; private set;}
		public LineShape(SVG svg, XmlNode node) : base(svg, node)
		{
			double x1 = XmlUtil.AttrValue(node, "x1", 0);
			double y1 = XmlUtil.AttrValue(node, "y1", 0);
			double x2 = XmlUtil.AttrValue(node, "x2", 0);
			double y2 = XmlUtil.AttrValue(node, "y2", 0);
			P1 = new Point(x1, y1);
			P2 = new Point(x2, y2);
		}
	}
	class PolylineShape : Shape
	{
		public Point[] Points {get; private set;}
		public PolylineShape(SVG svg, XmlNode node) : base(svg, node)
		{
			string points = XmlUtil.AttrValue(node, SVGTags.sPoints, string.Empty);
			ShapeUtil.StringSplitter split = new ShapeUtil.StringSplitter(points);
			List<Point> list = new List<Point>();
			while (split.More)
			{
				list.Add(split.ReadNextPoint());
			}
			Points = list.ToArray();
		}
	}
	class PolygonShape : Shape
	{
		static Fill DefaultFill = null;
		public override Fill Fill 
		{ 
			get 
			{
				Fill f = base.Fill;
				if (f == null)
					f = DefaultFill;
				return f;
			}
		}
		public Point[] Points {get; private set;}
		public PolygonShape(SVG svg, XmlNode node) : base(svg, node)
		{
			if (DefaultFill == null)
			{
				DefaultFill = new Fill(svg);
				DefaultFill.Color = svg.PaintServers.Parse("black");
			}

			string points = XmlUtil.AttrValue(node, SVGTags.sPoints, string.Empty);
			ShapeUtil.StringSplitter split = new ShapeUtil.StringSplitter(points);
			List<Point> list = new List<Point>();
			while (split.More)
			{
				list.Add(split.ReadNextPoint());
			}
			Points = list.ToArray();
		}
	}
	class UseShape : Shape
	{
		public double X { get; set; }
		public double Y { get; set; }
		public string hRef { get; set; }
		public UseShape(SVG svg, XmlNode node) : base(svg, node)
		{
			X = XmlUtil.AttrValue(node, "x", 0);
			Y = XmlUtil.AttrValue(node, "y", 0);
			hRef = XmlUtil.AttrValue(node, "xlink:href", string.Empty);
			if (hRef.StartsWith("#"))
				hRef = hRef.Substring(1);
		}
	}
	class ImageShape : Shape
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public ImageSource ImageSource {get; private set;}
		public ImageShape(SVG svg, XmlNode node) : base(svg, node)
		{
			X = XmlUtil.AttrValue(node, "x", 0);
			Y = XmlUtil.AttrValue(node, "y", 0);
			Width = XmlUtil.AttrValue(node, "width", 0);
			Height = XmlUtil.AttrValue(node, "height", 0);
			string hRef = XmlUtil.AttrValue(node, "xlink:href", string.Empty);
			if (hRef.Length > 0)
			{
				// filename given must be relative to the location of the svg file
				string svgpath = System.IO.Path.GetDirectoryName(svg.Filename);
				string filename = System.IO.Path.Combine(svgpath, hRef);

				BitmapImage b = new  BitmapImage();
				b.BeginInit();
				b.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);
				b.EndInit();
				ImageSource = b;
			}
		}
	}
	class Group : Shape
	{
		List<Shape> m_elements = new List<Shape>();
		public IList<Shape> Elements
		{
			get { return m_elements.AsReadOnly(); }
		}

		Shape AddChild(Shape shape)
		{
			m_elements.Add(shape);
			shape.Parent = this;
			return shape;
		}
		public Group(SVG svg, XmlNode node, Shape parent) : base(svg, node)
		{
			// parent on group must be set before children are added
			this.Parent = parent; 
			foreach (XmlNode childnode in node.ChildNodes)
			{
				Shape shape = AddToList(svg, m_elements, childnode, this);
				if (shape != null)
					shape.Parent = this;
			}
			if (Id.Length > 0)
				svg.AddShape(Id, this);
		}
		static public Shape AddToList(SVG svg, List<Shape> list, XmlNode childnode, Shape parent)
		{
			if (childnode.NodeType != XmlNodeType.Element)
				return null;
			if (childnode.Name == SVGTags.sShapeRect)
			{
				list.Add(new RectangleShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapeCircle)
			{
				list.Add(new CircleShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapeEllipse)
			{
				list.Add(new EllipseShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapeLine)
			{
				list.Add(new LineShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapePolyline)
			{
				list.Add(new PolylineShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapePolygon)
			{
				list.Add(new PolygonShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapePath)
			{
				list.Add(new PathShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapeGroup)
			{
				list.Add(new Group(svg, childnode, parent));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sLinearGradient)
			{
				svg.PaintServers.Create(childnode);
				return null;
			}
			if (childnode.Name == SVGTags.sRadialGradient)
			{
				svg.PaintServers.Create(childnode);
				return null;
			}
			if (childnode.Name == SVGTags.sDefinitions)
			{
				ReadDefs(svg, list, childnode);
				return null;
			}
			if (childnode.Name == SVGTags.sShapeUse)
			{
				list.Add(new UseShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == SVGTags.sShapeImage)
			{
				list.Add(new ImageShape(svg, childnode));
				return list[list.Count - 1];
			}
			if (childnode.Name == "text")
			{
				list.Add(new TextShape(svg, childnode, parent));
				return list[list.Count - 1];
			}
			return null;
		}
		static void ReadDefs(SVG svg, List<Shape> list, XmlNode node)
		{
			list = new List<Shape>(); // temp list, not needed. 
			//ShapeGroups defined in the 'def' section is added the the 'Shapes' dictionary in SVG for later reference
			foreach (XmlNode childnode in node.ChildNodes)
			{
				if (childnode.Name == SVGTags.sLinearGradient)
				{
					svg.PaintServers.Create(childnode);
					continue;
				}
				if (childnode.Name == SVGTags.sRadialGradient)
				{
					svg.PaintServers.Create(childnode);
					continue;
				}
				Group.AddToList(svg, list, childnode, null);
			}
		}
	}
}
