using System;
using System.Xml.Linq;

namespace SVGreek;

public abstract class Element
{
    public Point Position = new Point();
    public Color Fill = new Color();
    public Color Stroke = new Color();
    public double StrokeWidth = 1d;
    
    public (double, double)? TranslateFactor { get; protected set; }
    public (double, double)? ScaleFactor { get; protected set; }
    public (double, Point)? RotationFactor { get; protected set; }
    
    public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
    public ClipPath? ClipPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
    public abstract XElement ToXElement();
    public override string ToString() => ToXElement().ToString();

    public void Translate(object factor)
    {
        if (factor is ValueTuple<double, double> translateFactor)
        {
            TranslateFactor = translateFactor;
        }
        else
        {
            throw new ArgumentException("Invalid argument for Translate", nameof(factor));
        }
    }

    public string TranslateToString((double, double)? operation)
    {
        if (operation == null)
        {
            return "";
        }
        return "translate(" + operation.Value.Item1 + " " + operation.Value.Item2 + ")";
    }

    public void Scale(object factor)
    {
        if (factor is ValueTuple<double, double> scaleFactor)
        {
            ScaleFactor = scaleFactor;
        }
        else
        {
            throw new ArgumentException("Invalid argument for Scale", nameof(factor));
        }
    }
    
    public string ScaleFactorToString((double, double)? operation)
    {
        if (operation == null)
        {
            return "";
        }
        return "scale(" + operation.Value.Item1 + " " + operation.Value.Item2 + ")";
    }

    public void Rotate(object factor)
    {
        if (factor is ValueTuple<double, Point> rotationFactor)
        {
            RotationFactor = rotationFactor;
        }
        else
        {
            throw new ArgumentException("Invalid argument for Rotate", nameof(factor));
        }
    }
    
    public string RotateToString((double, Point)? operation)
    {
        if (operation == null)
        {
            return "";
        }
        return "rotate(" + operation.Value.Item1 + " " + operation.Value.Item2.X + " " + operation.Value.Item2.Y + ")";
    }

    public string TransformToString()
    {
        return TranslateToString(TranslateFactor) + ScaleFactorToString(ScaleFactor) + RotateToString(RotationFactor);
    }

    public static bool operator ==(Element element1, Element element2)
    {
        return element1.Equals(element2);
    }

    public static bool operator !=(Element element1, Element element2)
    {
        return element1 == element2;
    }
}