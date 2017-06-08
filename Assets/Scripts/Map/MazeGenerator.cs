using System.Collections.Generic;
using Assets.Scripts.Map;
using Assets.Scripts.Util;
using Assets.Scripts.World;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Created by:
// Jeffrey Wienen     
// S1079065 

namespace Assets.Scripts {
    public class MazeGenerator : MonoBehaviour {
        private readonly List<int> _rooms = new List<int>();
        private DisjointSet _disjointSet;

        private GridNode[,] _gridMap;

        private int _height;
        private Heap<GridNode> _prioList;
        private int _width;

        public GameObject EnemyPrefab;

        public int NodeSize = 12;
        public GameObject PrefabCorner;
        public GameObject PrefabCross;
        public GameObject PrefabDeadEnd;
        public GameObject PrefabRoomContent;
        public GameObject PrefabRoomCorner;
        public GameObject PrefabRoomEntrance;
        public GameObject PrefabStartEnd;
        public GameObject PrefabStraight;
        public GameObject PrefabThreeWay;
        public float PropPerNode = 0.1f;

        public GameObject[] Props;

        public bool ShowDebugNumbers;

        public GridNode[,] Init() {
            _height = GameManager.Instance.Size;
            _width = GameManager.Instance.Size;
            _disjointSet = new DisjointSet(_width, _height);
            _gridMap = new GridNode[_width, _height];
            _prioList = new Heap<GridNode>(_width * _height);

            GenerateMap();

            var chef = new DynamicOcclusion();
            _gridMap = chef.Bake(_gridMap);

            return _gridMap;
        }

        private void GenerateMap() {
            CreatePriorityList();
            CreateRooms();
            BuildHeapMaze();
            MakeMazeImperfect();
            ConfigurateNodes();
            CreateStartEndPoint();
            SetNodesPrefabs();
            InstantiateMap();
        }

        private void MakeMazeImperfect() {
            var amount = _height / 2;

            while (amount != 0) if (BreakWall()) amount--;
        }

        private bool BreakWall() {
            var x = GameManager.Instance.GetRandom(1, _width - 1);
            var y = GameManager.Instance.GetRandom(1, _height - 1);

            if (_gridMap[x, y].IsPartOfRoom) return false;
            if (_gridMap[x, y].HasWallDown && !_gridMap[x, y - 1].IsPartOfRoom) {
                _gridMap[x, y].HasWallDown = false;
                return true;
            }

            if (_gridMap[x, y].HasWallRight && !_gridMap[x + 1, y].IsPartOfRoom) {
                _gridMap[x, y].HasWallRight = false;
                return true;
            }

            return false;
        }

        private void CreateStartEndPoint() {
            // Remove the bottom wall from the starting location
            _gridMap[0, 0].NodeConfiguration -= 4;

            // Remove the top wall from the end location
            _gridMap[GameManager.Instance.Size - 1, GameManager.Instance.Size - 1].NodeConfiguration -= 1;
        }

        // For now, the amount of rooms will be calculated as follows:
        // The size of the maze divided by two minus one. So a maze 10x10 would have 4 rooms
        // inside it. SUBJECT TO CHANGE.
        private void CreateRooms() {
            var amount = GameManager.Instance.Size / 2 - 2;

            while (amount > 0) if (FindRoomPosition()) amount--;
        }

        private bool FindRoomPosition() {
            var startX = GameManager.Instance.GetRandom(1, GameManager.Instance.Size - 3);
            var startY = GameManager.Instance.GetRandom(1, GameManager.Instance.Size - 3);
            var startIndex = GetCoordIndex(startX, startY);

            if (_rooms.Contains(startIndex) || _rooms.Contains(startIndex + 1) ||
                _rooms.Contains(startIndex + _width) || _rooms.Contains(startIndex + _width + 1)) return false;
            _rooms.Add(startIndex);
            _rooms.Add(startIndex + 1);
            _rooms.Add(startIndex + _width + 1);
            _rooms.Add(startIndex + _width);
            CreateRoom(startX, startY);
            return true;
        }

        private void CreateRoom(int startX, int startY) {
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
            nodeTopLeft.IsTopSide = true;
            nodeTopRight.IsTopSide = true;
            
            // To prevent rooms getting placed above each other, we mark the cells around the room as 'room' aswell
            _rooms.Add(GetNodeIndex(_gridMap[nodeTopLeft.X, nodeTopLeft.Y + 1]));
            _rooms.Add(GetNodeIndex(_gridMap[nodeTopLeft.X + 1, nodeTopLeft.Y + 1]));
            _rooms.Add(GetNodeIndex(_gridMap[nodeBottomLeft.X, nodeBottomLeft.Y - 1]));
            _rooms.Add(GetNodeIndex(_gridMap[nodeBottomLeft.X + 1, nodeBottomLeft.Y - 1]));

            nodeBottomLeft.HasWallRight = false;
            nodeBottomLeft.HasWallDown = false;
            nodeTopLeft.HasWallDown = false;
            nodeTopLeft.HasWallRight = false;
            nodeTopRight.HasWallDown = false;

            // Dynamic Occlusion Fix
            var list = new List<GridNode> {
                nodeBottomLeft,
                nodeBottomRight,
                nodeTopLeft,
                nodeTopRight
            };

            nodeBottomLeft.RoomList = list;
            nodeBottomRight.RoomList = list;
            nodeTopLeft.RoomList = list;
            nodeTopRight.RoomList = list;
        }

        private void ConfigurateNodes() {
            for (var x = 0; x < _width; x++)
            for (var y = 0; y < _height; y++) _gridMap[x, y].NodeConfiguration = NodeConfig(_gridMap[x, y]);
        }

        private void SetNodesPrefabs() {
            for (var x = 0; x < _width; x++)
            for (var y = 0; y < _height; y++)
                if (_gridMap[x, y].IsPartOfRoom) SetRoomPrefab(_gridMap[x, y]);
                else SetDefaultPrefab(_gridMap[x, y]);
        }

        private void SetRoomPrefab(GridNode node) {
            switch (node.NodeConfiguration) {
                case 2:
                    node.Prefab = PrefabRoomEntrance;
                    if (node.IsTopSide) node.Scale = new Vector3(-1, 1, -1);
                    break;
                case 3:
                    node.Prefab = PrefabRoomCorner;
                    node.Rotation = -90;
                    break;
                case 6:
                    node.Prefab = PrefabRoomCorner;
                    break;
                case 8:
                    node.Prefab = PrefabRoomEntrance;
                    if (node.IsTopSide) node.Scale = new Vector3(1, 1, -1);
                    break;
                case 9:
                    node.Prefab = PrefabRoomCorner;
                    node.Rotation = 180;
                    break;
                case 12:
                    node.Prefab = PrefabRoomCorner;
                    node.Rotation = 90;
                    break;
                default:
                    node.Prefab = PrefabCross;
                    break;
            }
        }

        private void SetDefaultPrefab(GridNode node) {
            switch (node.NodeConfiguration) {
                case 0:
                    node.Prefab = PrefabCross;
                    break;
                case 1:
                    node.Prefab = PrefabThreeWay;
                    break;
                case 2:
                    node.Prefab = PrefabThreeWay;
                    node.Rotation = 90;
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
                    node.Prefab = PrefabCorner;
                    node.Rotation = 180;
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
            if (node.Y == _height - 1 || _gridMap[node.X, node.Y + 1].HasWallDown) config += 1;

            // Check if the current node is on the left edge
            if (node.X == 0 || _gridMap[node.X - 1, node.Y].HasWallRight) config += 8;

            if (node.HasWallRight) config += 2;

            if (node.Y == 0 || node.HasWallDown) config += 4;

            return config;
        }

        private void BuildHeapMaze() {
            while (_prioList.Count > 0) CheckCurrentNode(_prioList.RemoveFirst());
        }

        private void CheckCurrentNode(GridNode node) {
            if (_gridMap[node.X, node.Y].IsPartOfRoom) return;
            if (node.X < _width - 1) CheckNeighbour(node, _gridMap[node.X + 1, node.Y]);

            if (node.Y > 0) CheckNeighbour(node, _gridMap[node.X, node.Y - 1]);
        }

        private int GetCoordIndex(int x, int y) {
            return y * _width + x;
        }

        private int GetNodeIndex(GridNode node) {
            return node.Y * _width + node.X;
        }

        private void CheckNeighbour(GridNode nodeA, GridNode nodeB) {
            if (_gridMap[nodeB.X, nodeB.Y].IsPartOfRoom) return;

            var a = GetNodeIndex(nodeA);
            var b = GetNodeIndex(nodeB);
            if (_disjointSet.Find(a) == _disjointSet.Find(b)) {
                SetWall(nodeA, nodeB, true);
            }
            else {
                _disjointSet.Union(a, b);
                SetWall(nodeA, nodeB, false);
            }
        }

        private void SetWall(GridNode a, GridNode b, bool hasWall) {
            if (a.X + 1 == b.X) _gridMap[a.X, a.Y].HasWallRight = hasWall;
            else _gridMap[a.X, a.Y].HasWallDown = hasWall;
        }

        private void CreatePriorityList() {
            for (var x = 0; x < _width; x++)
            for (var y = 0; y < _height; y++) {
                var key = GameManager.Instance.GetRandom();
                var node = new GridNode {
                    X = x,
                    Y = y,
                    NodeConfiguration = 0,
                    HasWallRight = true,
                    HasWallDown = true,
                    Key = key
                };
                _prioList.Add(node);
                _gridMap[x, y] = node;
            }
        }

// Created By:
// Niek van den Brink
// S1078937
        private void CreateProp(GridNode node) {
            // If the semi-random number is within the range of the PropPerNode variable a prop will spawn in this node
            if (GameManager.Instance.GetRandom(0, 101) * 1.0f <= PropPerNode * 100.0f) {
                var children = node.Prefab.transform.Find("PropPlacement").GetComponentsInChildren<Transform>();

                // If the children.Length is 1 it only contains itself
                if (children.Length <= 1) return;

                // The actual children, if it has any, will start at index 1
                var child = children[GameManager.Instance.GetRandom(1, children.Length)];

                // Get a semi-random prop and place it at the previously gotten predefined prostion
                Instantiate(Props[GameManager.Instance.GetRandom(0, Props.Length)], child.transform.position,
                    transform.rotation);
            }
        }

// Created By:
// Niek van den Brink
// S1078937
        private void FillRoomWithContent(GridNode node) {
            var roomContent = Instantiate(PrefabRoomContent,
                node.Prefab.transform.position + new Vector3(6.0f, 0.0f, 6.0f), transform.rotation);
            roomContent.transform.Rotate(Vector3.up, node.Rotation);
            var locations = roomContent.transform.Find("EnemySpawns")
                .GetComponentsInChildren<Transform>();
            if (locations.Length <= 1) return;

            var spawnLoc = locations[Random.Range(1, locations.Length)];

            if (spawnLoc != null){
                Instantiate(EnemyPrefab, spawnLoc.position + new Vector3(0.0f, 1.0f, 0.0f),
                    transform.rotation);
            }
        }

        private void InstantiateMap() {
            // Set the default part of generated maze
            var offset = NodeSize * GameManager.Instance.Size / 2 - 6;
            for (var x = 0; x < _width; x++)
            for (var y = 0; y < _height; y++) {
                var position = new Vector3(x * NodeSize - offset, 0, y * NodeSize - offset);
                var node = _gridMap[x, y];

                node.Prefab = Instantiate(node.Prefab, position, transform.rotation);
                node.Prefab.transform.localScale = node.Scale;
                node.Prefab.transform.Rotate(Vector3.up, node.Rotation);

                // NodeConfiguration 2 and 8 are the entrances off a room, the content is placed based on these nodes
                if (node.IsPartOfRoom && (node.NodeConfiguration == 2 || node.NodeConfiguration == 8))
                    FillRoomWithContent(node);
                else CreateProp(node);
            }

            // Set the start and end location
            var start = Instantiate(PrefabStartEnd, new Vector3(0 - offset, 0, -12 - offset), transform.rotation);
            start.GetComponent<BoxCollider>().isTrigger = false;
            start.name = "Start";
            var end = Instantiate(PrefabStartEnd, new Vector3((_width - 1) * 12 - offset, 0, _height * 12 - offset),
                transform.rotation);
            end.name = "End";
            end.transform.Rotate(Vector3.up, 180);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos() {
            if (!ShowDebugNumbers) return;
            for (var x = 0; x < _width; x++)
            for (var y = 0; y < _height; y++) {
                if (!_gridMap[x, y].IsPartOfRoom) continue;
                Handles.color = Color.red;
                Handles.Label(new Vector3(x * NodeSize, 0, y * NodeSize),
                    "(" + GetNodeIndex(_gridMap[x, y]) + ")" + _gridMap[x, y].NodeConfiguration);
            }
        }

#endif
    }
}