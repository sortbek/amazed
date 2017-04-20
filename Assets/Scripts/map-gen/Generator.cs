using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts
{
    public class Generator : MonoBehaviour
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
        private Heap<GridNode> _prioList;
        private DisjointSet _disjointSet;
        private bool _isGenerated;


        public int NodeSize = 12;

        // Use this for initialization
        [ContextMenu("GenerateMap")]
        void Start ()
        {
            _height = MapSize;
            _width = MapSize;

            _disjointSet = new DisjointSet(_width, _height);

            _gridMap = new GridNode[_width,_height];

            _prioList = new Heap<GridNode>(_width * _height);

            GenerateMap();

            var chef = new DynamicOcclusion();
            _gridMap = chef.Bake(_gridMap);

            _isGenerated = true;
        }

        public bool IsGenerated()
        {
            return _isGenerated;
        }

        private void GenerateMap()
        {
            // Fill the dictionary with keys and GridNodes. The keys will represent
            // the priority in the queue later.
            CreatePriorityList();

            // Break down walls to build the actual maze
            BuildHeapMaze();

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
                    setPrefab(_gridMap[x, y]);
                }
            }
        }

        private void setPrefab(GridNode node)
        {

            switch (node.NodeConfiguration)
            {
                case 0:
                    node.Prefab = PrefabCorner;
                    break;
                case 1:
                    node.Prefab = PrefabThreeWay;
                    break;
                case 2:
                    node.Rotation = 90;
                    node.Prefab = PrefabThreeWay;
                    break;
                case 3:
                    node.Prefab = PrefabCorner;
                    node.Rotation = -90;
                    break;
                case 4:
                    node.Prefab = PrefabThreeWay;
                    node.Rotation = 180;
                    break;
                case 5:
                    node.Prefab = PrefabStraight;
                    break;
                case 6:
                    node.Prefab = PrefabCorner;
                    break;
                case 7:
                    node.Prefab = PrefabDeadEnd;
                    node.Rotation = -90;
                    break;
                case 8:
                    node.Prefab = PrefabThreeWay;
                    node.Rotation = -90;
                    break;
                case 9:
                    node.Rotation = 180;
                    node.Prefab = PrefabCorner;
                    break;
                case 10:
                    node.Prefab = PrefabStraight;
                    node.Rotation = 90;
                    break;
                case 11:
                    node.Prefab = PrefabDeadEnd;
                    node.Rotation = 180;
                    break;
                case 12:
                    node.Prefab = PrefabCorner;
                    node.Rotation = 90;
                    break;
                case 13:
                    node.Prefab = PrefabDeadEnd;
                    node.Rotation = 90;
                    break;
                case 14:
                    node.Prefab = PrefabDeadEnd;
                    break;
                case 15:
                    node.Prefab = PrefabCross;
                    break;
            }
        }

        private int NodeConfig(GridNode node)
        {
            var config = 0;

            // Check if the current node is on the top edge
            if (node.Y == _height -1  || _gridMap[node.X, node.Y + 1 ].HasWallDown)
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

            if (node.Y == 0 || node.HasWallDown)
            {
                config += 4;
            }

            return config;
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

            if (node.Y > 0)
            {
                CheckNeighbour(node, _gridMap[node.X , node.Y - 1]);
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
                    _prioList.Add(node);
                    _gridMap[x, y] = node;
                }
            }
        }

        private GridNode GetLowestFromHeap()
        {
            return _prioList.RemoveFirst();
        }

        public GridNode[,] GetMap()
        {
            return _gridMap;
        }

        private void InstantiateMap()
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var position = new Vector3(x * NodeSize, 0,y * NodeSize);
                    var node = _gridMap[x, y];
                    node.Prefab = Instantiate(node.Prefab, position, transform.rotation);
                    node.Prefab.transform.Rotate(Vector3.up, node.Rotation);
                    node.SetActive(false);
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
//                    Handles.color = Color.red;
//                    Handles.Label(new Vector3(x * NodeSize, 0 , (y * NodeSize)), ""+ _gridMap[x,y].NodeConfiguration  );
//                }
//            }
//        }

    }
}
