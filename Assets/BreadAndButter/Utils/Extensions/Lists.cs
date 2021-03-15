using System;
using System.Collections.Generic;

namespace BreadAndButter.Utils.Extensions
{
    public static class Lists
    {
        public static int BinarySearch<T>(this List<T> _list, T _value, IComparer<T> _comparer)
        {
            _list.Sort();

            int index = -1;

            int min = 0;
            int max = _list.Count;

            for(int i = 0; i < _list.Count; i++)
            {
                int midpoint = (min + max) / 2;

                int compared = _comparer.Compare(_list[midpoint], _value);
                if(compared > 0)
                {
                    min = midpoint;
                    continue;
                }
                
                if(compared < 0)
                {
                    max = midpoint;
                    continue;
                }

                if(compared == 0)
                {
                    index = midpoint;
                    break;
                }
            }

            return index;
        }

        public static bool Empty<T>(this List<T> _list) => _list.Count == 0;

        public static void QuickSort<T>(this List<T> _list, int _left, int _right) where T : IComparable
        {
            if(_left < _right)
            {
                int pivot = Partition(_list, _left, _right);

                if(pivot > 1)
                {
                    QuickSort(_list, _left, pivot - 1);
                }
                if (pivot + 1 < _right)
                {
                    QuickSort(_list, pivot + 1, _right);
                }
            }
        }

        private static int Partition<T>(List<T> _list, int _left, int _right) where T : IComparable
        {
            T pivot = _list[_left];

            while(true)
            {
                while(_list[_left].CompareTo(pivot) < 0)
                {
                    _left++;
                }

                while(_list[_right].CompareTo(pivot) > 0)
                {
                    _right--;
                }

                if(_left < _right)
                {
                    if (_list[_left].CompareTo(_list[_right]) == 0) return _right;

                    T temp = _list[_left];
                    _list[_left] = _list[_right];
                    _list[_right] = temp;
                }
                else
                {
                    return _right;
                }
            }
        }
    }
}