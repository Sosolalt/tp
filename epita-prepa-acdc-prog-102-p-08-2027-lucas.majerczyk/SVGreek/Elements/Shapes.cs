using System;
using System.Collections.Generic;
using SVGreek;

namespace SVGreek;

public class Triangle : Polygon
{
    public Triangle(Point point, double size, double strokeWidth = 1, Color? strokeColor = null, Color? fillColor = null)
    {
        StrokeWidth = strokeWidth;
        Stroke = strokeColor;
        Fill = fillColor;
        Connected = true;

        double halfSize = size / 2;
        double height = Math.Sqrt(3) / 2 * size;

        // calculate vertices of the equilateral triangle
        Point top = new Point(point.X, point.Y - height / 2);
        Point bottomLeft = new Point(point.X - halfSize, point.Y + height / 2);
        Point bottomRight = new Point(point.X + halfSize, point.Y + height / 2);

        // add vertices to the points list in clockwise order
        Points.Add(top);
        Points.Add(bottomRight);
        Points.Add(bottomLeft);

        Position = top;
    }
}


public class Hexagon : Polygon
{
    public Hexagon(Point point, double size, double strokeWidth = 1, Color? strokeColor = null, Color? fillColor = null)
    {
        StrokeWidth = strokeWidth;
        Stroke = strokeColor;
        Fill = fillColor;
        Connected = true;

        double height = Math.Sqrt(3) * size;
        double halfSize = size / 2;

        // calculate vertices of the hexagon
        Point topRight = new Point(point.X + halfSize, point.Y - height / 2);
        Point middleRight = new Point(point.X + size, point.Y);
        Point bottomRight = new Point(point.X + halfSize, point.Y + height / 2);
        Point bottomLeft = new Point(point.X - halfSize, point.Y + height / 2);
        Point middleLeft = new Point(point.X - size, point.Y);
        Point topLeft = new Point(point.X - halfSize, point.Y - height / 2);

        // add vertices to the points list in clockwise order
        Points.Add(topRight);
        Points.Add(middleRight);
        Points.Add(bottomRight);
        Points.Add(bottomLeft);
        Points.Add(middleLeft);
        Points.Add(topLeft);

        Position = topRight;
    }
}


public class Octagon : Polygon
{
    public Octagon(Point point, double size, double strokeWidth = 1, Color? strokeColor = null, Color? fillColor = null)
    {
        StrokeWidth = strokeWidth;
        Stroke = strokeColor;
        Fill = fillColor;
        Connected = true;

        double diagonal = size * Math.Sqrt(2);
        double halfDiagonal = diagonal / 2;
        double halfSize = size / 2;

        // calculate vertices of the octagon
        Point topRight = new Point(point.X + halfDiagonal, point.Y - halfSize);
        Point upperMiddleRight = new Point(point.X + halfSize, point.Y - diagonal);
        Point lowerMiddleRight = new Point(point.X + halfSize, point.Y + diagonal);
        Point bottomRight = new Point(point.X + halfDiagonal, point.Y + halfSize);
        Point bottomLeft = new Point(point.X - halfDiagonal, point.Y + halfSize);
        Point lowerMiddleLeft = new Point(point.X - halfSize, point.Y + diagonal);
        Point upperMiddleLeft = new Point(point.X - halfSize, point.Y - diagonal);
        Point topLeft = new Point(point.X - halfDiagonal, point.Y - halfSize);

        // add vertices to the points list in clockwise order
        Points.Add(topRight);
        Points.Add(upperMiddleRight);
        Points.Add(lowerMiddleRight);
        Points.Add(bottomRight);
        Points.Add(bottomLeft);
        Points.Add(lowerMiddleLeft);
        Points.Add(upperMiddleLeft);
        Points.Add(topLeft);

        Position = topRight;
    }
}

public class Star : Polygon
{
    public Star(Point point, double size, double strokeWidth = 1, Color? strokeColor = null, Color? fillColor = null)
    {
        StrokeWidth = strokeWidth;
        Stroke = strokeColor;
        fillColor = fillColor;
        Connected = true;

        double innerRadius = size / 2;
        double outerRadius = size;
        double angle = Math.PI / 2;
        double angleIncrement = Math.PI / 5;

        // calculate vertices of the star
        for (int i = 0; i < 10; i++)
        {
            double radius = i % 2 == 0 ? outerRadius : innerRadius;
            double x = point.X + radius * Math.Cos(angle);
            double y = point.Y - radius * Math.Sin(angle);
            Points.Add(new Point(x, y));
            angle += angleIncrement;
        }

        Position = Points[0];
    }
}



