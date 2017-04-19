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
        public GameObject PrefabDeadEnd;

        private int _height;
        private int _width;

        private GridNode[,] _gridMap;
        private Dictionary<int, GridNode> _tempMap = new Dictionary<int, GridNode>();
        private Heap<GridNode> _prioList;
        private DisjointSet _disjointSet;

        private int _nodeSize = 12;

        // Use this for initialization
        void Start ()
        {
            _height = MapSize;
            _width = MapSize;

            _disjointSet = new DisjointSet(_width, _height);

            _gridMap = new GridNode[_width,_height];

            _prioList = new Heap<GridNode>(_width * _height);

            GenerateMap();
        }

        // Update is called once per frame
        void Update () {
		
        }

        private void GenerateMap()
        {
            var sw = new Stopwatch();
            sw.Start();
            // Fill the dictionary with keys and GridNodes. The keys will represent
            // the priority in the queue later.
            CreatePriorityList();

            // Break down walls to build the actual maze
//            BuildMaze();

            BuildHeapMaze();

            // Set the right configuration for each node
            ConfigurateNodes();
            print("Map generated in: " + sw.ElapsedMilliseconds + " ms" );
            sw.Stop();
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
            if (node.Y == 0 || _gridMap[node.X, node.Y - 1].HasWallDown)
            {
                config += 1;
            }

            // Check if the current node is on the left edge
            if (node.X == 0 || _gridMap[node.X - 1, node.Y].HasWallRight)
            {
                config += 8;
            }

            if (node.HasWallRight)
            {
                config += 2;
            }

            if (node.HasWallDown)
            {
                config += 4;
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

        private void BuildHeapMaze()
        {
            while (_prioList.Count > 0)
            {
                CheckCurrentNode(GetLowestFromHeap());
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
                    var key = pseudoRandom.Next();
                    var node = new GridNode {X = x, Y = y, NodeConfiguration = 0, HasWallRight = true, HasWallDown = true, Key = key };
                    _tempMap.Add(key,node);
                    _prioList.Add(node);
                    _gridMap[x, y] = node;
                }
            }
        }

        private GridNode GetLowestFromHeap()
        {
            return _prioList.RemoveFirst();
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
                    var position = new Vector3(x * _nodeSize, 0,-(y * _nodeSize));
                    var node = _gridMap[x, y];
                    switch (node.NodeConfiguration)
                    {
                        case 0:
                            Instantiate(PrefabCross, position, transform.rotation);
                            break;
                        case 1:
                            Instantiate(PrefabThreeWay, position, transform.rotation);
                            break;
                        case 2:
                            Instantiate(PrefabThreeWay, position, transform.rotation).transform.Rotate(Vector3.up, 90);
                            break;
                        case 3:
                            Instantiate(PrefabCorner, position, transform.rotation).transform.Rotate(Vector3.up, -90);
                            break;
                        case 4:
                            Instantiate(PrefabThreeWay, position, transform.rotation).transform.Rotate(Vector3.up, 180);
                            break;
                        case 5:
                            Instantiate(PrefabStraight, position, transform.rotation);
                            break;
                        case 6:
                            Instantiate(PrefabCorner, position, transform.rotation);
                            break;
                        case 7:
                            Instantiate(PrefabDeadEnd, position, transform.rotation).transform.Rotate(Vector3.up, -90);
                            break;
                        case 8:
                            Instantiate(PrefabThreeWay, position,transform.rotation ).transform.Rotate(Vector3.up, -90);
                            break;
                        case 9:
                            Instantiate(PrefabCorner, position, transform.rotation).transform.Rotate(Vector3.up, 180);
                            break;
                        case 10:
                            Instantiate(PrefabStraight, position, transform.rotation).transform.Rotate(Vector3.up, 90);
                            break;
                        case 11:
                            Instantiate(PrefabDeadEnd, position, transform.rotation).transform.Rotate(Vector3.up, 180);
                            break;
                        case 12:
                            Instantiate(PrefabCorner, position, transform.rotation).transform.Rotate(Vector3.up, 90);
                            break;
                        case 13:
                            Instantiate(PrefabDeadEnd, position, transform.rotation).transform.Rotate(Vector3.up, 90);
                            break;
                        case 14:
                            Instantiate(PrefabDeadEnd, position, transform.rotation);
                            break;
                    }
                }
            }
        }

//        void OnDrawGizmos()
//        {
//            var nodeSize = 2;
//            for (var x = 0; x < _width; x++)
//            {
//                for (var y = 0; y < _height; y++)
//                {
//
//                    Handles.color = Color.red;
//                    Handles.Label(new Vector3(x * _nodeSize, 0 ,-(y * _nodeSize)), "" + _gridMap[x,y].NodeConfiguration );
//                }
//            }
//        }

    }
}
