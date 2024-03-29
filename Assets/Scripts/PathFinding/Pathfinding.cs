﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PathFinding;
using Assets.Scripts.Util;
using UnityEngine;


// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.pathfinding {
    public class Pathfinding : MonoBehaviour {
        private Grid _grid;

        private PathRequestManager _requestManager;

        private void Awake() {
            _requestManager = GetComponent<PathRequestManager>();
            _grid = GetComponent<Grid>();
        }


        public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
            StartCoroutine(FindPath(startPos, targetPos));
        }

        private Node GetNode(Vector3 position) {
            var node = _grid.NodeFromWorldPoint(position);

            return node.Walkable ? node : GetClosestWalkable(node);
        }

        private Node GetClosestWalkable(Node node, int depth = 1) {
            while (true) {
                var above = _grid.GetGrid()[node.GridX, node.GridY + depth];
                var right = _grid.GetGrid()[node.GridX + depth, node.GridY];
                var bottom = _grid.GetGrid()[node.GridX, node.GridY - depth];
                var left = _grid.GetGrid()[node.GridX - depth, node.GridY];
                if (above.Walkable)
                    return above;
                if (right.Walkable)
                    return right;
                if (bottom.Walkable)
                    return bottom;
                if (left.Walkable)
                    return left;
                depth = depth + 1;
            }
        }

        private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {
            var waypoints = new Vector3[0];
            var pathSuccess = false;

            var startNode = GetNode(startPos);
            var targetNode = GetNode(targetPos);


            if (startNode.Walkable && targetNode.Walkable) {
                var openSet = new Heap<Node>(_grid.MaxSize);
                var closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0) {
                    var currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode) {
                        pathSuccess = true;
                        break;
                    }

                    foreach (var neighbour in _grid.GetNeighbours(currentNode)) {
                        if (!neighbour.Walkable || closedSet.Contains(neighbour))
                            continue;

                        var newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                        if (newMovementCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour))
                            continue;
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            yield return null;
            if (pathSuccess)
                waypoints = RetracePath(startNode, targetNode);
            _requestManager.FinishedProcessingPath(waypoints, pathSuccess);
        }

        private Vector3[] RetracePath(Node startNode, Node endNode) {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode) {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            var waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            return waypoints;
        }

        protected Vector3[] SimplifyPath(List<Node> path) {
            var waypoints = new List<Vector3>();
            var directionOld = Vector2.zero;

            for (var i = 1; i < path.Count; i++) {
                var directionNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);
                if (directionNew != directionOld)
                    waypoints.Add(path[i].WorldPosition);
                directionOld = directionNew;
            }
            return waypoints.ToArray();
        }

        private static int GetDistance(Node nodeA, Node nodeB) {
            var dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            var dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}