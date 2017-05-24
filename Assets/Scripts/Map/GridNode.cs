using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

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
        public bool IsTopSide;
        public int X;
        public int Y;
        public int Key;
        public bool IsPartOfRoom;

        public List<GridNode> BakedList = new List<GridNode>();
        public List<GridNode> RoomList = new List<GridNode>();
        public GameObject Prop;
        public Vector3 Scale = new Vector3(1,1,1);
        public int Rotation;
        public GameObject Prefab;


        public void AddBakedNode(GridNode node) {
            BakedList.Add(node);
        }

        public void SetActive(bool isActive) {
            Prefab.SetActive(isActive);
            if (Prop != null) Prop.SetActive(isActive);
        }

        public int CompareTo(GridNode other) {
            return Key.CompareTo(other.Key);
        }

        public int HeapIndex { get; set; }
    }
}