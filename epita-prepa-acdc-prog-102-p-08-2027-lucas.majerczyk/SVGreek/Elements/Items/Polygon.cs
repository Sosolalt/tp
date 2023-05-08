using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SVGreek;

public class Polygon : Element
{
    public List<Point> Points;
    public bool Connected;

    public Polygon(List<Point> points, bool connected, Color? strokeColor = null, Color? fillColor = null,
        double strokeWidth = 1d)
    {
        Points = points;
        if (points.Count != 0)
        {
            Position = points[0];
        }
        else
        {
            Position = new Point();
        }
        Connected = connected;
        Stroke = strokeColor;
        StrokeWidth = strokeWidth;
        Fill = fillColor;
    }

    public Polygon(XElement element)
    {
        Points = new List<Point>();
        Connected = false;
        foreach (var attribute in element.Attributes())
        {
            switch (attribute.Name.LocalName)
            {
                case "polygon":
                    Connected = true;
                    break;
                case "polyline":
                    Connected = false;
                    break;
                case "points":
                    string input = attribute.Value.Replace(".", ",");
                    List<double> numbersList = input.Split(' ').Select(double.Parse).ToList();
                    
                    Points = toPoints(numbersList);
                    if (Points.Count != 0)
                    {
                        Position = Points[0];
                    }
                    else
                    {
                        Position = new Point();
                    }
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

    protected Polygon()
    {
        throw new NotImplementedException();
    }

    public List<Point> toPoints(List<double> list)
    {
        List<Point> answer = new List<Point>();
        if (list.Count % 2 != 0)
        {
            throw new InvalidAttributeException();
        }
        for (int i = 0; i < list.Count; i++)
        {
            answer.Add(new Point(list[0], list[1]));
            i++;
        }

        return answer;
    }

    public override XElement ToXElement()
    {
        string transform = "";

        transform +=  TranslateFactor == null ? "" :  TranslateFactor + " ";

        transform += ScaleFactor == null ? "" : ScaleFactor + " ";

        transform += RotationFactor == null ? "" : RotationFactor;

        XElement contacts =
            new XElement(Connected ? "polygone" : "polyline",
                new XAttribute("points", $"{pointsToString(Points)}"),
                new XAttribute("stroke", Stroke is null ? "none" : $"{Stroke}"),
                new XAttribute("fill", Fill is null ? "none" : $"{Fill}"),
                new XAttribute("stroke-width", StrokeWidth), transform  == "" ? null : 
                    new XAttribute("transform",TransformToString())
            );
        return contacts;    
    }

    public string pointsToString(List<Point> list)
    {
        string answer = "";
        for (int i = 0; i < list.Count; i++)
        {
            answer += list[i].X + " " + list[i].Y;
            if (i + 1 != list.Count)
            {
                answer += " ";
            }
        }

        return answer;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Polygon other)
        {
            return Position == other.Position
                   && Connected == other.Connected
                   && listEqual(Points, other.Points)
                   && Stroke == other.Stroke
                   && Fill == other.Fill
                   && (Math.Abs(StrokeWidth - other.StrokeWidth) < 0.05d);
        }
        return false;
    }

    public bool listEqual(List<Point> l1, List<Point> l2)
    {
        if (l1.Count != l2.Count)
        {
            return false;
        }

        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
            {
                return false;
            }
        }

        return true;
    }
}