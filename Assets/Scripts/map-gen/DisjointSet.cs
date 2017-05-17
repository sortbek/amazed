using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DisjointSet
    {
        public Dictionary<int, int> _set = new Dictionary<int, int>();

        public DisjointSet(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var key = (y * width) + x;
                    _set[key] = -1;
                }
            }
        }

        public int Find(int index)
        {
            if (_set[index] < 0)
            {
                return index;
            }
            return _set[index] = Find(_set[index]);
        }

        public void Union(int indexA, int indexB)
        {
            indexA = Find(indexA);
            indexB = Find(indexB);

            var newSize = _set[indexB] + _set[indexA];

            if (_set[indexA] > _set[indexB])
            {
                _set[indexB] = indexA;
                _set[indexA] = newSize;
            }
            else
            {
                _set[indexA] = indexB;
                _set[indexB] = newSize;
            }
        }
    }
}
