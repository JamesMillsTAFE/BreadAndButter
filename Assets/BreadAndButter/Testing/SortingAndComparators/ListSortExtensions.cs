using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class ListSortExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type needs to have a comparator.</typeparam>
    public static void BubbleSort<T>(this List<T> _list) where T : IComparable
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
    }
}
