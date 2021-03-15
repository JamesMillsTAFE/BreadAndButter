using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class ListExtensions
{
    /// <summary>
    /// Returns whether or not the list is completely empty.
    /// </summary>
    public static bool IsEmpty<T>(this List<T> _list) => _list.Count == 0;

    /// <summary>
    /// Generates a randomised version of the list and returns it.
    /// </summary>
    public static List<T> Randomise<T>(this List<T> _list)
    {
        // Copy the original list into a temporary one to prevent 
        // Any data lossage in the original
        List<T> listCopy = new List<T>();
        _list.ForEach(x => listCopy.Add(x));
        
        // Loop through the entire copied list until it is empty
        List<T> randomised = new List<T>();
        while(!listCopy.IsEmpty())
        {
            // Get a random index from the size of the list
            int index = UnityEngine.Random.Range(0, listCopy.Count);

            // Copy the item at the random index into the randomised list and 
            // remove it from the copied one
            randomised.Add(listCopy[index]);
            listCopy.RemoveAt(index);
        }

        return randomised;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type needs to have a comparator.</typeparam>
    public static List<T> BubbleSort<T>(this List<T> _list) where T : IComparable
    {
        T temp;

        for(int j = 0; j <= _list.Count - 2; j++)
        {
            for(int i = 0; i <= _list.Count - 2; i++)
            {
                IComparable first = _list[i];
                IComparable second = _list[i + 1];

                int comparison = first.CompareTo(second);
                // first is after second
                if(comparison > 0)
                {
                    temp = _list[i + 1];
                    _list[i + 1] = _list[i];
                    _list[i] = temp;
                }
            }
        }

        return _list;
    }
}
