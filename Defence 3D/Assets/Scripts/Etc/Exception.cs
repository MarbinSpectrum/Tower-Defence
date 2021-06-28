using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    public class Exception
    {
        #region[배열 범위초과 검사]
        public static bool IndexOutRange<T>(int x, int y, T[,] array)
        {
            if (x >= array.GetLength(0) || x < 0 || y >= array.GetLength(1) || y < 0)
                return false;
            return true;
        }

        public static bool IndexOutRange<T>(Vector2Int v, T[,] array)
        {
            return IndexOutRange<T>(v.x, v.y, array);
        }

        public static bool IndexOutRange<T>(int a, List<T> array)
        {
            if (array == null || a >= array.Count || a < 0)
                return false;
            return true;
        }

        public static bool IndexOutRange<T>(int a, T[] array)
        {
            if (a >= array.GetLength(0) || a < 0)
                return false;
            return true;
        }
        #endregion
    }

    public class AreaCheck
    {
        public static bool RectIn(Vector2 pos,Rect rect)
        {
            if (rect.x > pos.x || rect.x + rect.width < pos.x || rect.y < pos.y || rect.y - rect.height > pos.y)
                return false;
            return true;
        }

        public static bool RectIn(Vector2 pos, RectInt rect)
        {
            return RectIn(pos, new Rect(rect.x, rect.y, rect.width, rect.height));
        }
    }

    public class Fun
    {
        public static void Swap<T>(ref T a,ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}