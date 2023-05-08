namespace SVGreek;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class Canvas
{
    private double _width, _height;
    private (Point, double, double)? _viewBox;
    private List<Layer> _layers;
    private int _index;
    
    public int State { get; private set; }
    public List<(CanvasAction, object)> Actions { get; private set; }
    
    public delegate void CanvasAction(object arg);


    public Canvas(double width, double height, (Point, double, double)? viewBox = null)
    {
        _width = width;
        _height = height;
        _viewBox = viewBox;
        _layers = new List<Layer>();
        _index = -1;
    }

    public Canvas(XElement element)
    {
        _width = 0;
        _height = 0;
        _viewBox = null;
        _layers = new List<Layer>();
        _index = -1;

        if (element.Name.LocalName != "svg")
            throw new InvalidDocumentException();

        foreach (var attribute in element.Attributes())
        {
            if (attribute.Name == "width")
                _width = double.Parse(attribute.Value);
            else if (attribute.Name == "height")
                _height = double.Parse(attribute.Value);
            else if (attribute.Name == "viewBox")
            {
                var viewBox = attribute.Value.Split(' ').Select(double.Parse).ToList();
                if (viewBox.Count != 4)
                    throw new InvalidAttributeException();

                _viewBox = (new Point(viewBox[0], viewBox[1]), viewBox[2], viewBox[3]);
            }
            else
                throw new InvalidAttributeException();
        }
    }

    public Canvas(string path)
    {
        var doc = XDocument.Load(path);
        if (doc.Root == null || doc.Root.Name.LocalName != "svg")
            throw new InvalidDocumentException();

        _width = 0;
        _height = 0;
        _viewBox = null;
        _layers = new List<Layer>();
        _index = -1;

        foreach (var attribute in doc.Root.Attributes())
        {
            if (attribute.Name == "width")
                _width = double.Parse(attribute.Value);
            else if (attribute.Name == "height")
                _height = double.Parse(attribute.Value);
            else if (attribute.Name == "viewBox")
            {
                var viewBox = attribute.Value.Split(' ').Select(double.Parse).ToList();
                if (viewBox.Count != 4)
                    throw new InvalidAttributeException();

                _viewBox = (new Point(viewBox[0], viewBox[1]), viewBox[2], viewBox[3]);
            }
            else
                throw new InvalidAttributeException();
        }
    }

    public Layer? ActiveLayer
    {
        get
        {
            if (_index == -1)
                return null;
            else
                return _layers[_index];
        }
    }

    public int Index
    {
        get { return _index; }
        set
        {
            if (value >= _layers.Count)
                throw new IndexOutOfRangeException();

            _index = value;
        }
    }

    public void Add(object element)
    {
        if (!(element is Element))
            throw new InvalidArgumentException();

        if (_layers.Count == 0)
        {
            _layers.Add(new Layer());
            _index = 0;
        }

        ActiveLayer?.Add((Element)element);
    }

    public void Remove(object element)
    {
        if (!(element is Element))
            throw new InvalidArgumentException();

        ActiveLayer?.Remove((Element)element);
    }

    public void TopLayer()
    {
        if (_layers.Count == 0)
            _index = -1;
        else
            _index = _layers.Count - 1;
    }

    public void AddLayer(Layer? layer = null)
    {
        if (layer == null)
            _layers.Add(new Layer());
        else
            _layers.Add(layer);

        TopLayer();
    }

    public XElement ToXElement()
    {
        var svg = new XElement("svg",
            new XAttribute("width", _width),
            new XAttribute("height", _height));

        if (_viewBox != null)
        {
            svg.Add(new XAttribute("viewBox",
                $"{_viewBox.Value.Item1.X} {_viewBox.Value.Item1.Y} " +
                $"{_viewBox.Value.Item2} {_viewBox.Value.Item3}"));
        }

        foreach (var layer in _layers)
        {
            foreach (var element in layer.Elements)
            {
                svg.Add(element.ToXElement());
            }
        }

        return svg;
    }

    public void Save(string path)
    {
        var doc = new XDocument(ToXElement());
        doc.Save(path);
    }

    public static Canvas operator +(Canvas canvas1, Canvas canvas2)
    {
        var result = new Canvas(canvas1._width, canvas1._height, canvas1._viewBox);

        foreach (var layer in canvas1._layers)
        {
            if (layer.Elements.Count > 0)
                result.AddLayer(layer);
        }

        foreach (var layer in canvas2._layers)
        {
            if (layer.Elements.Count > 0 && result._layers.All(l => l != layer))
                result.AddLayer(layer);
        }

        return result;
    }

    public static Canvas operator -(Canvas canvas1, Canvas canvas2)
    {
        var result = new Canvas(canvas1._width, canvas1._height, canvas1._viewBox);

        foreach (var layer1 in canvas1._layers)
        {
            var layer2 = canvas2._layers.Find(l => l.ToString() == layer1.ToString());

            if (layer2 == null)
            {
                result.AddLayer(layer1);
            }
            else
            {
                var newLayer = new Layer();
                foreach (var element1 in layer1.Elements)
                {
                    if (!layer2.Elements.Contains(element1))
                        newLayer.Add(element1);
                }

                if (newLayer.Elements.Count > 0)
                    result.AddLayer(newLayer);
            }
        }

        return result;
    }

    public void SwitchLayers(int index1, int index2)
    {
        throw new NotImplementedException();
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }

    public void Redo()
    {
        throw new NotImplementedException();
    }

    public void Do(CanvasAction action, object argument)
    {
        throw new NotImplementedException();
    }

    public void Apply()
    {
        throw new NotImplementedException();
    }

    public static void Push(object canvas)
    {
        throw new NotImplementedException();
    }

    public static void Restore(Canvas canvas)
    {
        throw new NotImplementedException();
    }

    public List<string> LsBranch(out string currentBranch)
    {
        throw new NotImplementedException();
    }

    public static void Branch(object arguments)
    {
        throw new NotImplementedException();
    }

    public static void Checkout(Canvas canvas, string branchName)
    {
        throw new NotImplementedException();
    }

    public static void Merge(Canvas canvas, string branchName)
    {
        throw new NotImplementedException();
    }

    public static Canvas[] ParseAll(string path)
    {
        throw new NotImplementedException();
    }
}

