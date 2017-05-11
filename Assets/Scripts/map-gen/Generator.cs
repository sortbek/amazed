using System;
using Assets.Scripts.World;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts
{
    public class Generator : MonoBehaviour
    {
        public GameObject PrefabCross;
        public GameObject PrefabStraight;
        public GameObject PrefabCorner;
        public GameObject PrefabThreeWay;
        public GameObject PrefabDeadEnd;
        public GameObject PrefabStartEnd;

        private int _height;
        private int _width;

        private GridNode[,] _gridMap;
        private Heap<GridNode> _prioList;
        private DisjointSet _disjointSet;
        private bool _isGenerated;


        public int NodeSize = 12;

        void Awake()
        {
            GameManager.Instance.Size = 10;
            GameManager.Instance.GameSeed = "test";
        }
        // Use this for initialization
        [ContextMenu("GenerateMap")]
        void Start ()
        {
            _height = GameManager.Instance.Size;
            _width = GameManager.Instance.Size;

            _disjointSet = new DisjointSet(_width, _height);

            _gridMap = new GridNode[_width,_height];

            _prioList = new Heap<GridNode>(_width * _height);

            GenerateMap();

            var chef = new DynamicOcclusion();
            _gridMap = chef.Bake(_gridMap);

            _isGenerated = true;
            GetComponent<MapManager>().Init();
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

            // Creating predefined rooms
            CreateRooms();

            // Break down walls to build the actual maze
            BuildHeapMaze();

            // Remove walls to make the maze imperfect and generate loops
            MakeMazeImperfect();

            // Set the right configuration for each node
            ConfigurateNodes();

            // Create start and end point
            CreateStartEndPoint();

            // Set the prefab for each node
            SetNodesPrefabs();

            // Instantiate the map
            InstantiateMap();


        }

        private void MakeMazeImperfect()
        {
            var amount = _height / 2;

            while (amount != 0)
            {
                if (BreakWall())
                {
                    amount--;
                }
            }
        }

        private bool BreakWall()
        {
            var x = GameManager.Instance.GetRandom(1 , _width -1);
            var y = GameManager.Instance.GetRandom(1, _height -1);

            if (_gridMap[x, y].IsPartOfRoom)
            {
                return false;
            }
            if (_gridMap[x,y].HasWallDown && !_gridMap[x,y-1].IsPartOfRoom)
            {
                _gridMap[x, y].HasWallDown = false;
                return true;
            }

            if (_gridMap[x, y].HasWallRight && !_gridMap[x+1,y].IsPartOfRoom)
            {
                _gridMap[x, y].HasWallRight = false;
                return true;
            }

            return false;
        }

        private void CreateStartEndPoint()
        {

            // Remove the bottom wall from the starting location
            _gridMap[0, 0].NodeConfiguration -= 4;

            // Remove the top wall from the end location
            _gridMap[GameManager.Instance.Size-1,GameManager.Instance.Size-1 ].NodeConfiguration -= 1;
        }

        private void CreateRooms()
        {
            var maxSize = _height - 3;
            CreateRoom(1, 1);
            CreateRoom(1, maxSize);
            CreateRoom(maxSize, maxSize);
            CreateRoom(maxSize, 1);
        }

        private void CreateRoom(int startX, int startY)
        {
            var nodeX = startX;
            var nodeY = startY;

            var nodeBottomLeft = _gridMap[nodeX, nodeY];
            var nodeBottomRight = _gridMap[nodeX + 1, nodeY];

            var nodeTopLeft = _gridMap[nodeX, nodeY + 1];
            var nodeTopRight = _gridMap[nodeX + 1, nodeY + 1];


            _disjointSet.Union(GetNodeIndex(nodeBottomLeft), GetNodeIndex(nodeBottomRight));
            _disjointSet.Union(GetNodeIndex(nodeBottomRight), GetNodeIndex(nodeTopRight));
            _disjointSet.Union(GetNodeIndex(nodeTopRight), GetNodeIndex(nodeTopLeft));

            nodeBottomLeft.IsPartOfRoom = true;
            nodeBottomRight.IsPartOfRoom = true;
            nodeTopLeft.IsPartOfRoom = true;
            nodeTopRight.IsPartOfRoom = true;

            nodeBottomLeft.HasWallRight = false;
            nodeBottomLeft.HasWallDown = false;
            nodeTopLeft.HasWallDown = false;
            nodeTopLeft.HasWallRight = false;
            nodeTopRight.HasWallDown = false;
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

        private void SetNodesPrefabs()
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    SetPrefab(_gridMap[x, y]);
                }
            }
        }

        private void SetPrefab(GridNode node)
        {

            switch (node.NodeConfiguration)
            {
                case 0:
                    node.Prefab = PrefabCross;
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
            if (_gridMap[node.X, node.Y].IsPartOfRoom) return;
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
            if (_gridMap[nodeB.X, nodeB.Y].IsPartOfRoom) return;

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
            if (GameManager.Instance.RandomSeed)
            {
                GameManager.Instance.GameSeed = Guid.NewGuid().ToString().Replace("-", "");
            }

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y <_height; y++)
                {
                    var key = GameManager.Instance.GetRandom();
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
            // Set the default part of generated maze
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var position = new Vector3(x * NodeSize, 0,y * NodeSize);
                    var node = _gridMap[x, y];
                    node.Prefab = Instantiate(node.Prefab, position, transform.rotation);
                    node.Prefab.transform.Rotate(Vector3.up, node.Rotation);
                }
            }

            // Set the start and end location
            var start = Instantiate(PrefabStartEnd, new Vector3(0, 0, -12), transform.rotation);

            var end = Instantiate(PrefabStartEnd, new Vector3((_width - 1) * 12, 0, _height * 12), transform.rotation);
            end.transform.Rotate(Vector3.up, 180);
        }

//        #if UNITY_EDITOR
//        void OnDrawGizmos()
//        {
//            var nodeSize = 2;
//            for (var x = 0; x < _width; x++)
//            {
//                for (var y = 0; y < _height; y++)
//                {
//                    Handles.color = Color.red;
//                    Handles.Label(new Vector3(x * NodeSize, 0 , (y * NodeSize)), "("+ GetNodeIndex(_gridMap[x,y])+")"+ _gridMap[x,y].NodeConfiguration);
//                }
//            }
//        }
//        #endif
    }
}