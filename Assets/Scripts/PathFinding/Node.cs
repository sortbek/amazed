using Assets.Scripts.Util;
using UnityEngine;


// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.PathFinding
{
    public class Node : IHeapItem<Node>
    {

        public bool Walkable;
        public Vector3 WorldPosition;
        public int GridX;
        public int GridY;

        public int GCost;
        public int HCost;
        public Node Parent;

        public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
        {
            Walkable = walkable;
            WorldPosition = worldPos;
            GridX = gridX;
            GridY = gridY;
        }

        public int FCost
        {
            get
            {
                return GCost + HCost;
            }
        }

        public int HeapIndex { get; set; }

        public int CompareTo(Node nodeToCompare)
        {
            var compare = FCost.CompareTo(nodeToCompare.FCost);
            if (compare == 0)
            {
                compare = HCost.CompareTo(nodeToCompare.HCost);
            }
            return -compare;
        }

    }
}
