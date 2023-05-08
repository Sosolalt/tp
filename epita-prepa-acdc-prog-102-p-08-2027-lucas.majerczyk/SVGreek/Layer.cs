using System;
using System.Collections;
using System.Collections.Generic;

namespace SVGreek
{
    public class Layer : IEnumerable, IEnumerator
    {
        public int Index { get; private set; } = -1;
        public List<Element> Elements { get; } = new List<Element>();
        
        public Element this[int index] => Elements[index];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Layer(){}
        public Layer GetEnumerator()
        {
            return this;
        }
        
        /*public Element Current()
        {
            return Elements[Index];
        }
        */
        public Element Current => throw new NotImplementedException();
        
        object IEnumerator.Current => Current;


        public bool MoveNext() => ++Index < Elements.Count;

        public void Reset() => Index = Elements.Count == 0 ? -1 : 0;

        public int Count => Elements.Count;

        public void Add(Element element)
        {
            Elements.Add(element);
        }

        public void Remove(Element element)
        {
            Elements.Remove(element);
        }

        public bool Contains(Element element)
        {
            return Elements.Contains(element);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Element> FilterShape<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Element> FilterColor(Color color)
        {
            throw new NotImplementedException();
        }

        public void UpsideDown()
        {
            throw new NotImplementedException();
        }

        public void FillMeUp()
        {
            throw new NotImplementedException();
        }
    }
}