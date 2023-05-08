using System;
using System.Xml.Linq;

namespace SVGreek;

public class Ellipse : Element
{
    public (double X, double Y) R;

    public Ellipse(Point position, double rx, double ry, Color? strokeColor = null, Color? fillColor = null,
        double strokeWidth = 1d)
    {
        Position = position;
        R = (rx, ry);
        Stroke = strokeColor;
        Fill = fillColor;
        StrokeWidth = strokeWidth;
    }

    public Ellipse(Point position, double r, Color? strokeColor = null, Color? fillColor = null,
        double strokeWidth = 1d)
    {
        Position = position;
        R = (r, r);
        Stroke = strokeColor;
        Fill = fillColor;
        StrokeWidth = strokeWidth;
    }

    public Ellipse(XElement element)
    {
        Position = new Point();
        R = (0, 0);
        bool circle = false;
        foreach (var attribute in element.Attributes())
        {
            switch (attribute.Name.LocalName)
            {
                case "cx":
                    Position.X = int.Parse(attribute.Value);
                    break;
                case "r":
                    R = (int.Parse(attribute.Value), int.Parse(attribute.Value));
                    break;
                case "cy":
                    Position.Y = int.Parse(attribute.Value);
                    break;
                case "rx":
                    R.X = int.Parse(attribute.Value);
                    break;
                case "ry":
                    R.Y = int.Parse(attribute.Value);
                    break;
                case "stroke":
                    Stroke = new Color(attribute.Value);
                    break;
                case "fill":
                    Fill = new Color (attribute.Value);
                    break;
                case "stroke-width":
                    StrokeWidth = double.Parse(attribute.Value);
                    break;
                default:
                    throw new InvalidAttributeException();            }
        }
    }
    public override XElement ToXElement()
    {
        string transform = "";

        transform +=  TranslateFactor == null ? "" :  TranslateFactor + " ";

        transform += ScaleFactor == null ? "" : ScaleFactor + " ";
       
        transform += RotationFactor == null ? "" : RotationFactor;

        XElement contacts =
                new XElement(Math.Abs(R.X - R.Y) < 0.05d ? "circle" : "ellipse",
                new XAttribute("cx", $"{Position.X}"),
                new XAttribute("cy", $"{Position.Y}"),
                new XAttribute("rx", $"{R.X}"),
                new XAttribute("ry", $"{R.Y}"),
                new XAttribute("stroke", Stroke is null ? "none" : $"{Stroke}"),
                new XAttribute("fill", Fill is null ? "none" : $"{Fill}"),
                new XAttribute("stroke-width", StrokeWidth), transform  == "" ? null : 
                    new XAttribute("transform",TransformToString())
            );
        return contacts;    
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Ellipse other)
        {
            return Position == other.Position
                   && R == other.R
                   && Stroke == other.Stroke
                   && Fill == other.Fill
                   && (Math.Abs(StrokeWidth - other.StrokeWidth) < 0.05d);
        }
        return false;
    }
}