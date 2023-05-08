using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SVGreek;

public class ClipPath  : Group
{
    public ClipPath(List<Element> elements) : base(elements) { }

    public ClipPath(XElement element) : base(element)
    {
        throw new NotImplementedException();
    }

    public override XElement ToXElement()
    {
        throw new NotImplementedException();
    }
}