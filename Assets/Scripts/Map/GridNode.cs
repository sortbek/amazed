using System;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;


// Created by:
// Jeffrey Wienen
// S1079065
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
        public List<GridNode> BakedList = new List<GridNode>();
        public bool HasWallDown = true;
        public bool HasWallRight = true;
        public bool IsPartOfRoom;
        public bool IsTopSide;
        public int Key;
        public int NodeConfiguration;
        public GameObject Prefab;
        public GameObject Prop;
        public List<GridNode> RoomList = new List<GridNode>();
        public int Rotation;
        public Vector3 Scale = new Vector3(1, 1, 1);
        public int X;
        public int Y;

        public int CompareTo(GridNode other) {
            return Key.CompareTo(other.Key);
        }

        public int HeapIndex { get; set; }


        public void AddBakedNode(GridNode node) {
            BakedList.Add(node);
        }

        public void SetActive(bool isActive) {
            Prefab.SetActive(isActive);
            if (Prop != null) Prop.SetActive(isActive);
        }
    }
}