using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [Flags]
    public enum Walls
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8
    }


    public class GridNode : IHeapItem<GridNode>
    {
        public int NodeConfiguration;
        public bool HasWallDown;
        public bool HasWallRight;
        public int X;
        public int Y;
        public int Key;

        public void Configurate()
        {

        }

        public int CompareTo(GridNode other)
        {
            return Key.CompareTo(other.Key);
        }

        public int HeapIndex { get; set; }
    }
}