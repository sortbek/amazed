using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts {
    [Flags]
    public enum Walls {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8
    }

    public class GridNode : IHeapItem<GridNode> {
        public int NodeConfiguration;
        public bool HasWallDown = true;
        public bool HasWallRight = true;
        public int X;
        public int Y;
        public int Key;
        public bool IsPartOfRoom;
        private Generator _generator = Object.FindObjectOfType<Generator>();

        public List<int> NoTop = new List<int> {
            0, 2, 4, 6, 8, 10, 12, 14
        };

        public List<int> NoRight = new List<int> {
            0, 1, 4, 5, 8, 9, 12, 13
        };

        public List<int> NoBottom = new List<int> {
            0, 1, 2, 3, 8, 9, 10, 11
        };

        public List<int> NoLeft = new List<int> {
            0, 1, 2, 3, 4, 5, 6, 7
        };

        public List<GridNode> BakedList = new List<GridNode>();
        public GameObject Prefab;

        public int Rotation;

        public void AddBakedNode(GridNode node) {
            BakedList.Add(node);
        }

        public void SetActive(bool isActive) {
            Prefab.SetActive(isActive);
        }

        public int CompareTo(GridNode other) {
            return Key.CompareTo(other.Key);
        }

        public int HeapIndex { get; set; }

        public List<GridNode> GetAdjacentNodes() {

            List<GridNode> adjacentNodes = new List<GridNode>();

            if (_generator.IsGenerated()) {
                if (NoTop.Contains(NodeConfiguration))
                    adjacentNodes.Add(_generator.GridMap[X, Y - 1]);

                if (NoRight.Contains(NodeConfiguration))
                    adjacentNodes.Add(_generator.GridMap[X + 1, Y]);

                if (NoBottom.Contains(NodeConfiguration))
                    adjacentNodes.Add(_generator.GridMap[X, Y + 1]);

                if (NoLeft.Contains(NodeConfiguration))
                    adjacentNodes.Add(_generator.GridMap[X - 1, Y]);
                return adjacentNodes;
            }
            return adjacentNodes;
        }
    }
}