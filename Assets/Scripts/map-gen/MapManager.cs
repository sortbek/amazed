using System;
using Assets.Scripts.World;
using UnityEngine;
using Assets.Scripts.PathFinding;

namespace Assets.Scripts {
    public class MapManager : MonoBehaviour {
        public GameObject Ground;

        private GridNode[,] _map;
        private Generator _generator;
        private GameObject _player;

        private GridNode _current;
        private GridNode _newCurrent;

        // Map settings
        public int Size;
        public string Seed;
        public bool IsRandom;

        // Use this for initialization
        void Start() {
            _generator = GetComponent<Generator>();
            _player = GameObject.FindWithTag("player");

            Init();

        }

        // Update is called once per frame
        void Update() {
            // Check every frame the current node the player is in. If the player walks into a new node, we must enable
            // the neighbour prefabs that were baked into the node with Dynamic Occlusion Culling
            CurrentLocation();
            if (_newCurrent != _current) {
                UpdateCulling();
            }
        }

        private void SetSettings() {
            if (IsRandom) {
                Seed = Guid.NewGuid().ToString().Replace("-", "");
            }
            GameManager.Instance.GameSeed = Seed;
            GameManager.Instance.Size = Size;
        }

        public void Init() {

            // Set the initial settings for the map like Size and Seed
            SetSettings();

            // The ground is set dynamically depending on the map and node size
            SetGround();

            // Generate the grid
            var map = _generator.Init();


            _map = map;
            _newCurrent = _map[0, 0];

            // Position the player on the map
            GameManager.Instance.Load();

            // Create the graph we need for A*
            GetComponentInChildren<Grid>().Init();

            // Enable the Dynamic Occlusion Culling
            EnableCulling();
        }

        private void SetGround() {
            var size = GameManager.Instance.Size * _generator.NodeSize + 100;
            Ground.transform.position = new Vector3(size / 2 - 50, 0, size / 2 - 50);
            Ground.transform.localScale = new Vector3(size, .1f, size);
        }

        public void EnableCulling() {
            foreach (var m in _map) {
                m.SetActive(false);
            }
        }

        private void CurrentLocation() {
            var x = (int)Math.Round(_player.transform.position.x / _generator.NodeSize);
            var y = (int)Math.Round(_player.transform.position.z / _generator.NodeSize);

            if (x < 0 || y < 0 || x > GameManager.Instance.Size || y > GameManager.Instance.Size) {
                return;
            }
            if (_current == null || x != _current.X || y != _current.Y) {
                _newCurrent = _map[x, y];
            }
        }

        private void UpdateCulling() {
            //        // Disable the previous enabled baked
            if (_current != null) {
                foreach (var node in _current.BakedList) {
                    node.SetActive(false);
                }
            }

            _current = _newCurrent;
            _current.SetActive(true);

            foreach (var node in _current.BakedList) {
                node.SetActive(true);
            }
        }
    }
}