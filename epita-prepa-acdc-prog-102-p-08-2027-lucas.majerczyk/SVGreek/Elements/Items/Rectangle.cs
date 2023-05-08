using System;
using System.Xml.Linq;

namespace SVGreek;

public class Rectangle : Element
{
    public double Width;
    public double Height;

    public Rectangle(Point position, double width, double height, Color? strokeColor = null, Color? fillColor = null,
        double strokeWidth = 1d)
    {
        Position = position;
        Width = width;
        Height = height;
        Stroke = strokeColor;
        Fill = fillColor;
        StrokeWidth = strokeWidth;
    }

    public Rectangle(XElement element)
    {
        Position = new Point();
        
        foreach (var attribute in element.Attributes())
        {
            switch (attribute.Name.LocalName)
            {
                case "x":
                    Position.X = int.Parse(attribute.Value);
                    break;
                case "y":
                    Position.Y = int.Parse(attribute.Value);
                    break;
                case "width":
                    Width = int.Parse(attribute.Value);
                    break;
                case "height":
                    Height = int.Parse(attribute.Value);
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
                    throw new InvalidAttributeException();
            }
        }
    }
    public override XElement ToXElement()
    {
        string transform = "";

        transform +=  TranslateFactor == null ? "" :  TranslateFactor + " ";

        transform += ScaleFactor == null ? "" : ScaleFactor + " ";

        transform += RotationFactor == null ? "" : RotationFactor;

        XElement contacts =
            new XElement("rect",
                new XAttribute("x", $"{Position.X}"),
                new XAttribute("y", $"{Position.Y}"),
                new XAttribute("width", $"{Width}"),
                new XAttribute("height", $"{Height}"),
                new XAttribute("stroke", Stroke is null ? "none" : $"{Stroke}"),
                new XAttribute("fill", Fill is null ? "none" : $"{Fill}"),
                new XAttribute("stroke-width", StrokeWidth), transform  == "" ? null : 
                    new XAttribute("transform",TransformToString())
            );
        return contacts;    
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Rectangle other)
        {
            return Position == other.Position
                   && Math.Abs(Width - other.Width) < 0.05d
                   && Math.Abs(Height - other.Height) < 0.05d
                   && Stroke == other.Stroke
                   && Fill == other.Fill
                   && (Math.Abs(StrokeWidth - other.StrokeWidth) < 0.05d);
        }
        return false;
    }    
}