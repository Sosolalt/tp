using System;

namespace SVGreek;

public class Point
{
    public double X;
    public double Y;
    
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point()
    {
        X = 0;
        Y = 0;
    }

    public static bool operator ==(Point a, Point b)
    {
        if ( Math.Abs(a.X - b.X) < 0.05d &&  Math.Abs(a.Y - b.Y) < 0.05d)
        {
            return true;
        }

        return false;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }
}