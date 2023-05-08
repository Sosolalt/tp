using System;
using System.Collections.Generic;

namespace SVGreek
{
    public abstract class Tool
    {
        public Canvas Canvas;

        public static Point? LineIntersection((Point, Point) line1, (Point, Point) line2)
        {
            throw new NotImplementedException();
        }

        private static int ComputeOrientation(Point p1, Point p2, Point p3)
        {
            throw new NotImplementedException();
        }
    }

    public class Eyedropper : Tool
    {
        private readonly Canvas canvas;

        public Eyedropper(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public Color Eyedrop(Point point)
        {
            throw new NotImplementedException();
        }
    }

    public class Eraser : Tool
    {
        private readonly Canvas canvas;

        public Eraser(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Erase(Line line)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectionTool : Tool

    {
    public SelectionTool(Canvas canvas)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Element> Select(Polygon polygon)
    {
        throw new NotImplementedException();
    }
    }
}