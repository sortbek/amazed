using System;
using Assets.Scripts.Util;
using Assets.Scripts.World;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts {
    public class Generator : MonoBehaviour {
        public GameObject PrefabCross;
        public GameObject PrefabStraight;
        public GameObject PrefabCorner;
        public GameObject PrefabThreeWay;
        public GameObject PrefabDeadEnd;

        private int _height;
        private int _width;

        public GridNode[,] GridMap;
        private Heap<GridNode> _prioList;
        private DisjointSet _disjointSet;
        private bool _isGenerated;


        public int NodeSize = 12;

        void Awake() {
            GameManager.Instance.Size = 10;
            GameManager.Instance.GameSeed = "test";
        }
        // Use this for initialization
        [ContextMenu("GenerateMap")]
        void Start() {
            _height = GameManager.Instance.Size;
            _width = GameManager.Instance.Size;

            _disjointSet = new DisjointSet(_width, _height);

            GridMap = new GridNode[_width, _height];

            _prioList = new Heap<GridNode>(_width * _height);

            GenerateMap();

            var chef = new DynamicOcclusion();
            GridMap = chef.Bake(GridMap);

            _isGenerated = true;
            GetComponent<MapManager>().Init(GridMap);
        }

        public bool IsGenerated() {
            return _isGenerated;
        }

        private void GenerateMap() {
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

            // Instantiate the map
            InstantiateMap();


        }

        private void MakeMazeImperfect() {
            var amount = _height / 2;

            while (amount != 0) {
                if (BreakWall()) {
                    amount--;
                }
            }
        }

        private bool BreakWall() {
            var x = GameManager.Instance.GetRandom(1, _width - 1);
            var y = GameManager.Instance.GetRandom(1, _height - 1);

            if (GridMap[x, y].IsPartOfRoom) {
                return false;
            }

            if (GridMap[x, y].HasWallDown && !GridMap[x, y - 1].IsPartOfRoom) {
                GridMap[x, y].HasWallDown = false;
                return true;
            }

            if (GridMap[x, y].HasWallRight && !GridMap[x + 1, y].IsPartOfRoom) {
                GridMap[x, y].HasWallRight = false;
                return true;
            }

            return false;
        }

        private void CreateRooms() {
            var maxSize = _height - 3;
            CreateRoom(1, 1);
            CreateRoom(1, maxSize);
            CreateRoom(maxSize, maxSize);
            CreateRoom(maxSize, 1);
        }

        private void CreateRoom(int startX, int startY) {
            var nodeX = startX;
            var nodeY = startY;

            var nodeBottomLeft = GridMap[nodeX, nodeY];
            var nodeBottomRight = GridMap[nodeX + 1, nodeY];

            var nodeTopLeft = GridMap[nodeX, nodeY + 1];
            var nodeTopRight = GridMap[nodeX + 1, nodeY + 1];


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

        private void ConfigurateNodes() {
            for (var x = 0; x < _width; x++) {
                for (var y = 0; y < _height; y++) {
                    GridMap[x, y].NodeConfiguration = NodeConfig(GridMap[x, y]);
                    SetPrefab(GridMap[x, y]);
                }
            }
        }

        private void SetPrefab(GridNode node) {

            switch (node.NodeConfiguration) {
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

        private int NodeConfig(GridNode node) {
            var config = 0;

            // Check if the current node is on the top edge
            if (node.Y == _height - 1 || GridMap[node.X, node.Y + 1].HasWallDown) {
                config += 1;
            }

            // Check if the current node is on the left edge
            if (node.X == 0 || GridMap[node.X - 1, node.Y].HasWallRight) {
                config += 8;
            }

            if (node.HasWallRight) {

                config += 2;
            }

            if (node.Y == 0 || node.HasWallDown) {
                config += 4;
            }

            return config;
        }

        private void BuildHeapMaze() {
            while (_prioList.Count > 0) {
                CheckCurrentNode(GetLowestFromHeap());
            }

        }

        private void CheckCurrentNode(GridNode node) {
            if (GridMap[node.X, node.Y].IsPartOfRoom) return;
            if (node.X < _width - 1) {
                CheckNeighbour(node, GridMap[node.X + 1, node.Y]);
            }

            if (node.Y > 0) {
                CheckNeighbour(node, GridMap[node.X, node.Y - 1]);
            }
        }

        private int GetNodeIndex(GridNode node) {
            return (node.Y * _width) + node.X;
        }

        private void CheckNeighbour(GridNode nodeA, GridNode nodeB) {
            if (GridMap[nodeB.X, nodeB.Y].IsPartOfRoom) return;

            var a = GetNodeIndex(nodeA);
            var b = GetNodeIndex(nodeB);
            if (_disjointSet.Find(a) == _disjointSet.Find(b)) {
                SetWall(nodeA, nodeB, true);
            } else {
                _disjointSet.Union(a, b);
                SetWall(nodeA, nodeB, false);
            }
        }

        private void SetWall(GridNode a, GridNode b, bool hasWall) {
            if (a.X + 1 == b.X) {
                GridMap[a.X, a.Y].HasWallRight = hasWall;
            } else {
                GridMap[a.X, a.Y].HasWallDown = hasWall;
            }
        }

        private void CreatePriorityList() {
            if (GameManager.Instance.RandomSeed) {
                GameManager.Instance.GameSeed = Guid.NewGuid().ToString().Replace("-", "");
            }

            for (var x = 0; x < _width; x++) {
                for (var y = 0; y < _height; y++) {
                    var key = GameManager.Instance.GetRandom();
                    var node = new GridNode { X = x, Y = y, NodeConfiguration = 0, HasWallRight = true, HasWallDown = true, Key = key };
                    _prioList.Add(node);
                    GridMap[x, y] = node;
                }
            }
        }

        private GridNode GetLowestFromHeap() {
            return _prioList.RemoveFirst();
        }

        public GridNode[,] GetMap() {
            return GridMap;
        }

        private void InstantiateMap() {
            for (var x = 0; x < _width; x++) {
                for (var y = 0; y < _height; y++) {
                    var position = new Vector3(x * NodeSize, 0, y * NodeSize);
                    var node = GridMap[x, y];
                    node.Prefab = Instantiate(node.Prefab, position, transform.rotation);
                    node.Prefab.transform.Rotate(Vector3.up, node.Rotation);
                }
            }
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