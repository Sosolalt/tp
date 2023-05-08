using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SVGreek;

public class Group : Element
{
    public List<Element> Elements;

    public Group(List<Element> elements)
    {
        Elements = elements;
    }

    public Group(XElement data)
    {
        throw new NotImplementedException();
    }

    public override XElement ToXElement()
    {
        throw new NotImplementedException();
    }

}