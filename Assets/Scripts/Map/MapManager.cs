using System;
using Assets.Scripts.Map.Weather;
using Assets.Scripts.PathFinding;
using Assets.Scripts.World;
using UnityEngine;

// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.Map {
    public class MapManager : MonoBehaviour {
        private GridNode _current;

        private GridNode[,] _map;
        private MazeGenerator _mazeGenerator;
        private GridNode _newCurrent;
        private int _offset;
        private GameObject _player;

        private WeatherManager _weatherManager;
        public GameObject Ground;
        
        public bool IsRandom;
        public string Seed;

        // Map settings
        public int Size;

        // Use this for initialization
        private void Start() {
            _mazeGenerator = GetComponent<MazeGenerator>();
            _player = GameObject.FindWithTag("player");
            _weatherManager = GameObject.Find("WeatherStation").GetComponent<WeatherManager>();
            Init();
        }

        // Update is called once per frame
        private void Update() {
            // Check every frame the current node the player is in. If the player walks into a new node, we must enable
            // the neighbour prefabs that were baked into the node with Dynamic Occlusion Culling
            CurrentLocation();
            if (_newCurrent != _current) UpdateCulling();
        }

        private void SetSettings() {
            if (IsRandom) Seed = Guid.NewGuid().ToString().Replace("-", "");
            GameManager.Instance.GameSeed = Seed;
        }

        public void Init() {
            // Set the initial settings for the map like Size and Seed
            SetSettings();

            // The ground is set dynamically depending on the map and node size
            SetGround();

            // Generate the grid
            var map = _mazeGenerator.Init();

            _map = map;
            _newCurrent = _map[0, 0];

            // Position the player on the map
            GameManager.Instance.Load();

            // Create the graph we need for A*
            GetComponentInChildren<Grid>().Init();

            EnableCulling();

            _weatherManager.Init();

            _offset = 12 * GameManager.Instance.Size / 2 - 6;
        }

        private void SetGround() {
            var size = GameManager.Instance.Size * _mazeGenerator.NodeSize + 20;
            Ground.transform.localScale = new Vector3(size, .1f, size);
        }

        public void EnableCulling() {
            foreach (var m in _map) m.SetActive(false);
        }

        private void CurrentLocation() {
            var x = (int) Math.Round((_player.transform.position.x + _offset) / _mazeGenerator.NodeSize);
            var y = (int) Math.Round((_player.transform.position.z + _offset) / _mazeGenerator.NodeSize);
            
            if (x < 0 || y < 0 || x > GameManager.Instance.Size - 1 || y > GameManager.Instance.Size - 1) return;
            if (_current == null || x != _current.X || y != _current.Y) {
                _newCurrent = _map[x, y];
                var player = GameManager.Instance.Character;
                if (player != null) player.Node = _newCurrent;
            }
        }

        private void UpdateCulling() {
            //        // Disable the previous enabled baked
            if (_current != null) foreach (var node in _current.BakedList) node.SetActive(false);

            _current = _newCurrent;
            _current.SetActive(true);

            foreach (var node in _current.BakedList) node.SetActive(true);
        }
    }
}