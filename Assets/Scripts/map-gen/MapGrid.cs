using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts
{

/*
    Node Configuration:

    Top
    Right
    Bottom
    Left

*/

    public class MapGrid : MonoBehaviour
    {
        public int MapSize;
        public string MapSeed;
        public bool UseRandomSeed;


        public GameObject PrefabCross;
        public GameObject PrefabStraight;
        public GameObject PrefabCorner;
        public GameObject PrefabThreeWay;

        private int _height;
        private int _width;

        private GridNode[,] _gridMap;
        private Dictionary<int, GridNode> _tempMap = new Dictionary<int, GridNode>();
        private DisjointSet _disjointSet;

        // Use this for initialization
        void Start ()
        {
            _height = MapSize;
            _width = MapSize;

            _disjointSet = new DisjointSet(_width, _height);

            _gridMap = new GridNode[_width,_height];

            var sw = new Stopwatch();
            sw.Start();
            GenerateMap();
        }

        // Update is called once per frame
        void Update () {
		
        }

        private void GenerateMap()
        {
            // Fill the dictionary with keys and GridNodes. The keys will represent
            // the priority in the queue later.
            CreatePriorityList();

            // Break down walls to build the actual maze
            BuildMaze();

            // Set the right configuration for each node
            ConfigurateNodes();

            // Instantiate the map
            InstantiateMap();

        }

        private void ConfigurateNodes()
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    _gridMap[x, y].NodeConfiguration = NodeConfig(_gridMap[x, y]);
                }
            }
        }

        private int NodeConfig(GridNode node)
        {
            var config = 0;

            // Check if the current node is on the top edge
            if (node.Y == 0)
            {
                config += 4;
            }
            else
            {
                // If we're not on the top edge, check if the node above us has a wall down.
                config += (_gridMap[node.X, node.Y - 1].HasWallDown)? 4 : 0 ;
            }

            // Check if the current node is on the left edge
            if (node.X == 0)
            {
                config += 8;
            }
            else
            {

                config += (_gridMap[node.X -1, node.Y].HasWallRight)? 8 : 0 ;
            }

            if (node.HasWallRight)
            {
                config += 2;
            }

            if (node.HasWallDown)
            {
                config += 1;
            }

            return config;
        }

        private void BuildMaze()
        {
            while (_tempMap.Count > 0)
            {
                CheckCurrentNode(GetLowestFromDictionary());
            }
        }

        private void CheckCurrentNode(GridNode node)
        {
            if (node.X < _width - 1)
            {
                CheckNeighbour(node, _gridMap[node.X + 1, node.Y]);
            }

            if (node.Y < _height - 1)
            {
                CheckNeighbour(node, _gridMap[node.X , node.Y + 1]);
            }
        }

        private int GetNodeIndex(GridNode node)
        {
            return (node.Y * _width) + node.X ;
        }

        private void CheckNeighbour(GridNode nodeA, GridNode nodeB)
        {
            var a = GetNodeIndex(nodeA);
            var b = GetNodeIndex(nodeB);
            if (_disjointSet.Find(a) == _disjointSet.Find(b))
            {
                SetWall(nodeA, nodeB, true);
            }
            else
            {
                _disjointSet.Union(a, b);
                SetWall(nodeA, nodeB, false);
            }
        }

        private void SetWall(GridNode a, GridNode b, bool hasWall)
        {
            if (a.X + 1 == b.X)
            {
                _gridMap[a.X, a.Y].HasWallRight = hasWall;
            }
            else
            {
                _gridMap[a.X, a.Y].HasWallDown = hasWall;
            }
        }

        private void CreatePriorityList()
        {

            if (UseRandomSeed)
            {
                MapSeed = Guid.NewGuid().ToString().Replace("-", "");
            }
            var pseudoRandom = new System.Random(MapSeed.GetHashCode());

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y <_height; y++)
                {
                    var node = new GridNode {X = x, Y = y, NodeConfiguration = 0, HasWallRight = true, HasWallDown = true};
                    _tempMap.Add(pseudoRandom.Next(),node);
                    _gridMap[x, y] = node;
                }
            }
        }

        private GridNode GetLowestFromDictionary()
        {
            // TODO: Implement Heap method for this. This Part takes way to long
            var lowest = int.MaxValue;
            var key = -1;
            var n = new GridNode();
            foreach (var col in _tempMap)
            {
                if (col.Key >= lowest) continue;
                n = col.Value;
                lowest = col.Key;
                key = col.Key;
            }
            _tempMap.Remove(key);
            return n;
        }

        public GridNode[,] GetMap()
        {
            return _gridMap;
        }

        private void InstantiateMap()
        {
            var t = GetComponent<Canvas>();
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var position = new Vector3(x * 8, 0,-(y * 8));
                    var node = _gridMap[x, y];
                    switch (node.NodeConfiguration)
                    {
//                        case 0:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
                        case 1:
                            Instantiate(PrefabThreeWay, position,transform.rotation ).transform.Rotate(Vector3.forward, -90);
                            break;
                        case 2:
                            Instantiate(PrefabThreeWay, position,transform.rotation );
                            break;
                        case 3:
                            Instantiate(PrefabCorner, position,transform.rotation ).transform.Rotate(Vector3.forward, -90);
                            break;
                        case 4:
                            Instantiate(PrefabThreeWay, position,transform.rotation ).transform.Rotate(Vector3.forward, 90);

                            break;
                        case 5:
                            Instantiate(PrefabStraight, position, transform.rotation).transform.Rotate(Vector3.forward, 90);
                            break;
                        case 6:
                            Instantiate(PrefabCorner, position, transform.rotation);
                            break;
                        case 7:
//                            Instantiate(PrefabCross, position, transform.rotation);
                            break;
//                        case 8:
//                            Instantiate(PrefabThreeWay, position,transform.rotation ).transform.Rotate(Vector3.forward, 180);
//                            break;
//                        case 9:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
//                        case 10:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
//                        case 11:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
//                        case 12:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
//                        case 13:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
//                        case 14:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
//                        case 15:
//                            Instantiate(PrefabCross, position, transform.rotation);
//                            break;
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            var nodeSize = 2;
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {

                    Handles.color = Color.red;
                    Handles.Label(new Vector3(x * 8, 0 ,-(y * 8)), "" + _gridMap[x,y].NodeConfiguration );
//                    var node = _gridMap[x, y];
//
//                    switch (node.NodeConfiguration)
//                    {
//                        case 0:
//
//                            break;
//                        case 1:
//
//                            break;
//                    }
//                    //Draw top gizmos
//                    Gizmos.color = Color.white;
//                    Gizmos.DrawCube(new Vector3(x * 2, -(y * 2), 0), Vector3.one );
//
//                    Gizmos.color = (node.HasWallRight)? Color.black : Color.white;
//                    Gizmos.DrawCube(new Vector3(x * 2 + 1,  -(y * 2), 0), Vector3.one );
//
//                    //Draw bottom gizmos
//                    Gizmos.color = (node.HasWallDown)? Color.black : Color.white;
//                    Gizmos.DrawCube(new Vector3(x * 2,  -(y * 2 + 1), 0), Vector3.one / 2 );
//                    Gizmos.DrawCube(new Vector3(x * 2 + 1,  -(y * 2 + 1), 0), Vector3.one  / 2 );
                }
            }
        }

    }
}
