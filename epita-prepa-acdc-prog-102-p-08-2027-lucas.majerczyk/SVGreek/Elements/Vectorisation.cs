
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp.PixelFormats;

namespace SVGreek;
using SixLabors.ImageSharp;

public static class Vectorisation
{
    public static bool HasWhiteNeighbour(Image<Rgba32> image, int x, int y)
    {
        throw new NotImplementedException();
    }
    
    public static void Spread(ref Image<Rgba32> image, int x, int y, List<Point> shape)
    {
        throw new NotImplementedException();
    }

    public static List<List<Point>> FindShapes(ref Image<Rgba32> image)
    {
        throw new NotImplementedException();
    }

    public static void RemoveUseless(Image<Rgba32> image, List<List<Point>> points)
    {
        throw new NotImplementedException();
    }

    public static Point GetClosestPoint(Point curr, List<Point> pointList)
    {
        throw new NotImplementedException();
    }

    public static List<Polygon> ExtractPolygon(List<List<Point>> Shapes)
    {
        throw new NotImplementedException();
    }
}

