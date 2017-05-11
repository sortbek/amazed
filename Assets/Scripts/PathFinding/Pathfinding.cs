using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts.PathFinding;
using Assets.Scripts.Util;

namespace Assets.Scripts.pathfinding
{
    public class Pathfinding : MonoBehaviour
    {

        private PathRequestManager _requestManager;
        private Grid _grid;

        void Awake()
        {
            _requestManager = GetComponent<PathRequestManager>();
            _grid = GetComponent<Grid>();
        }


        public void StartFindPath(Vector3 startPos, Vector3 targetPos)
        {
            StartCoroutine(FindPath(startPos,targetPos));
        }

        private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
        {
            var waypoints = new Vector3[0];
            var pathSuccess = false;

            var startNode = _grid.NodeFromWorldPoint(startPos);
            var targetNode = _grid.NodeFromWorldPoint(targetPos);


            if (startNode.Walkable && targetNode.Walkable)
            {
                var openSet = new Heap<Node>(_grid.MaxSize);
                var closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    var currentNode = openSet.RemoveFirst();
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (var neighbour in _grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.Walkable || closedSet.Contains(neighbour))
                        {
                            continue;
                        }

                        var newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                        if (newMovementCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour))
                        {
                            continue;
                        }
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
            {
                waypoints = RetracePath(startNode,targetNode);
            }
            _requestManager.FinishedProcessingPath(waypoints,pathSuccess);

        }

        private Vector3[] RetracePath(Node startNode, Node endNode)
        {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            var waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            return waypoints;

        }

        protected Vector3[] SimplifyPath(List<Node> path)
        {
            var waypoints = new List<Vector3>();
            var directionOld = Vector2.zero;

            for (var i = 1; i < path.Count; i ++)
            {
                var directionNew = new Vector2(path[i-1].GridX - path[i].GridX,path[i-1].GridY - path[i].GridY);
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i].WorldPosition);
                }
                directionOld = directionNew;
            }
            return waypoints.ToArray();
        }

        private static int GetDistance(Node nodeA, Node nodeB)
        {
            var dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            var dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (dstX > dstY)
                return 14*dstY + 10* (dstX-dstY);
            return 14*dstX + 10 * (dstY-dstX);
        }
    }
}