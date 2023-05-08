
namespace SVGreek;

using System;
using System.Collections.Generic;

public class Dataset<T>
{
   private List<T> _data;

   public Dataset(List<T> data)
   {
      _data = data;
   }

   public int Count(Predicate<T> predicate)
   {
      int count = 0;
      foreach (T item in _data)
      {
         if (predicate(item))
         {
            count++;
         }
      }
      return count;
   }

   public List<T> Filter(Predicate<T> predicate)
   {
      List<T> filteredList = new List<T>();
      foreach (T item in _data)
      {
         if (predicate(item))
         {
            filteredList.Add(item);
         }
      }
      return filteredList;
   }

   public Canvas ToHistogram(string title, (string, Predicate<T>)[] conditions)
   {
      throw new NotImplementedException();
   }

   public Canvas ToLineGraph(string title, (string, Predicate<T>)[] conditions)
   {
      throw new NotImplementedException();
   }

   public static Polygon GetSquareSlice(Point center, double radius, double angle)
   {
      throw new NotImplementedException();
   }

   public Canvas ToPieChart(string title, (string, Predicate<T>)[] conditions)
   {
      throw new NotImplementedException();
   }
}
