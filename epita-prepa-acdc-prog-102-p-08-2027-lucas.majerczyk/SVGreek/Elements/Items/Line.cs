using System;
using System.Xml.Linq;

namespace SVGreek;

public class Line : Element
{
    public Point Destination;

    public Line(Point source, Point destination, Color? strokeColor = null, double strokeWidth = 1d)
    {
        Position = source;
        Destination = destination;
        Stroke = strokeColor;
        StrokeWidth = strokeWidth;
    }

    public Line(XElement data)
    {
        Destination = new Point();
        Position = new Point();
        foreach (var attribute in data.Attributes())
        {
            switch (attribute.Name.LocalName)
            {
                case "x1":
                    Position.X = int.Parse(attribute.Value);
                    break;
                case "y1":
                    Position.Y = int.Parse(attribute.Value);
                    break;
                case "x2":
                    Destination.X = int.Parse(attribute.Value);
                    break;
                case "y2":
                    Destination.Y = int.Parse(attribute.Value);
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
            new XElement("line",
                new XAttribute("x1", $"{Position.X}"),
                new XAttribute("y1", $"{Position.Y}"),
                new XAttribute("x2", $"{Destination.X}"),
                new XAttribute("y2", $"{Destination.Y}"),
                new XAttribute("stroke", Stroke.ToString() == "none" ? "none" : $"{Stroke}"),
                new XAttribute("fill", Fill.ToString() == "none" ? "none" : $"{Position.X}"),
                new XAttribute("stroke-width", StrokeWidth), transform  == "" ? null : 
                new XAttribute("transform",TransformToString())
            );
        return contacts;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Line other)
        {
            return Position == other.Position
                   && Destination == other.Destination
                   && Stroke == other.Stroke
                   && Fill == other.Fill
                   && (Math.Abs(StrokeWidth - other.StrokeWidth) < 0.05d);
        }
        return false;
    }

}