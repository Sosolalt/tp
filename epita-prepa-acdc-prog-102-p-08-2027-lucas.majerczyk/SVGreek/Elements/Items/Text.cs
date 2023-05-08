using System;
using System.Xml.Linq;

namespace SVGreek
{
    public class Text : Element
    {
        public string Content { get; }

        public Text(Point point, string content, Color? strokeColor = null, Color? fillColor = null, double strokeWidth = 1d)
        {
            Position = point;
            Content = content;
            Stroke = strokeColor ?? new Color("none");
            Fill = fillColor ?? new Color("none");
            StrokeWidth = strokeWidth;
        }

        public Text(XElement element)
        {
            Position = new Point(
                double.Parse(element.Attribute("x")?.Value ?? "0"),
                double.Parse(element.Attribute("y")?.Value ?? "0")
            );
            Stroke = new Color(element.Attribute("stroke")?.Value ?? "none");
            Fill = new Color(element.Attribute("fill")?.Value ?? "none");
            StrokeWidth = double.Parse(element.Attribute("stroke-width")?.Value ?? "1");
            Content = element.Value;
        }

        public override XElement ToXElement()
        {
            var element = new XElement("text");
            element.SetAttributeValue("x", Position.X);
            element.SetAttributeValue("y", Position.Y);
            element.SetAttributeValue("stroke", Stroke.ToString());
            element.SetAttributeValue("fill", Fill.ToString());
            element.SetAttributeValue("stroke-width", StrokeWidth);
            element.Value = Content;
            if (TransformToString() != "")
            {
                element.SetAttributeValue("transform", TransformToString());
            }
            return element;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Text other))
            {
                return false;
            }

            return Position.Equals(other.Position) &&
                   Content == other.Content &&
                   Stroke.Equals(other.Stroke) &&
                   Fill.Equals(other.Fill) &&
                   Math.Abs(StrokeWidth - other.StrokeWidth) < double.Epsilon;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Content);
        }
    }
}
