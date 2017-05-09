using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PathFinding {
    public class PathFinder : MonoBehaviour {
        private GridNode _start;
        private GridNode _end;

        public PathFinder(GridNode start, GridNode end) {
            _start = start;
            _end = end;
        }

        public int GetDistance(GridNode start, GridNode end) {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }
    }
}
