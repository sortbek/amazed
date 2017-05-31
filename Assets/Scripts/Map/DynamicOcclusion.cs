using System.Collections.Generic;

namespace Assets.Scripts.Map
{
    public class DynamicOcclusion
    {
        private GridNode[,] _generatedMap;

        private readonly List<int> _openTop = new List<int>{0,1,2,3,8,9,10,11};
        private readonly List<int> _openRight = new List<int> {0,1,2,3,4,5,6,7};
        private readonly List<int> _openBottom = new List<int> {0, 2, 4, 6, 8, 10, 12, 14};
        private readonly List<int> _openLeft = new List<int> {0, 1, 4, 5, 9, 8, 12, 13};

        private GridNode _currentChecking;

        public GridNode[,] Bake(GridNode[,] map)
        {
            _generatedMap = map;
            for (var x = 0; x < _generatedMap.GetLength(0); x++)
            {
                for (var y = 0; y < _generatedMap.GetLength(1); y++)
                {
                    BakeNode(_generatedMap[x,y]);

                    _generatedMap[x,y] = _currentChecking;

                }
            }
            return _generatedMap;
        }

        private void BakeNode(GridNode node)
        {
            _currentChecking = node;
            var topBaked = node;
            var rightBaked = node;
            var bottomBaked = node;
            var leftBaked = node;

            // Keep going top untill we reach a point that is not in line of sight
            while (topBaked != null)
            {
                topBaked = GetNextTopVisible(topBaked);
            }

            while (rightBaked != null)
            {
                rightBaked = GetNextRightVisible(rightBaked);
            }

            while (bottomBaked != null)
            {
                bottomBaked = GetNextBottomVisible(bottomBaked);
            }

            while (leftBaked != null)
            {
                leftBaked = GetNextLeftVisible(leftBaked);
            }
        }

        private GridNode GetNextTopVisible(GridNode node)
        {   if (node.IsPartOfRoom)
            {
                AddSquareArea(node);
            }
            // Check if the current node we're checking is on the top row. If so, return null because we can't go higher
            if (node.Y == _generatedMap.GetLength(1) - 1 )
            {
                return null;
            }

            if(_openTop.IndexOf(_generatedMap[node.X, node.Y + 1].NodeConfiguration) == -1 )
            {
                return null;
            }
            _currentChecking.AddBakedNode(_generatedMap[node.X, node.Y + 1]);

            return _generatedMap[node.X, node.Y + 1];
        }

        private GridNode GetNextRightVisible(GridNode node)
        {
            if (node.IsPartOfRoom)
            {
                AddSquareArea(node);
            }
            // Check if the current node we're checkking is on the right edge of the map. If that's the case, we don't
            // need to go any further, because we reached the end.
            if (node.X == _generatedMap.GetLength(0) - 1 || _openRight.IndexOf(_generatedMap[node.X + 1, node.Y].NodeConfiguration) == -1 )
            {
                return null;
            }
            _currentChecking.AddBakedNode(_generatedMap[node.X + 1, node.Y]);

            return _generatedMap[node.X + 1, node.Y];
        }

        private GridNode GetNextBottomVisible(GridNode node)
        {   if (node.IsPartOfRoom)
            {
                AddSquareArea(node);
            }
            if (node.Y == 0 || _openBottom.IndexOf(_generatedMap[node.X, node.Y - 1].NodeConfiguration) == -1)
            {
                return null;
            }


                _currentChecking.AddBakedNode(_generatedMap[node.X, node.Y - 1]);


            return _generatedMap[node.X, node.Y - 1];
        }

        private GridNode GetNextLeftVisible(GridNode node)
        {
            if (node.IsPartOfRoom)
            {
                AddSquareArea(node);
            }
            if (node.X == 0 || _openLeft.IndexOf(_generatedMap[node.X - 1, node.Y].NodeConfiguration) == -1)
            {
                return null;
            }
                _currentChecking.AddBakedNode(_generatedMap[node.X - 1, node.Y]);


            return _generatedMap[node.X - 1, node.Y];
        }

        private void AddSquareArea(GridNode node)
        {
            _currentChecking.BakedList.AddRange(node.RoomList);
        }
    }
}