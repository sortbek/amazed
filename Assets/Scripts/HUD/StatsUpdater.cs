using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class StatsUpdater : MonoBehaviour {
        private Slider _health;
        private Character.Character _player;
        public Text Points, CurrentLevel;

        // Use this for initialization
        private void Start() {
            _health = GetComponentInChildren<Slider>();

            foreach (var text in GetComponentsInChildren<Text>())
                switch (text.name) {
                    case "PointsAmount":
                        Points = text;
                        break;
                    case "CurrentLevel":
                        CurrentLevel = text;
                        break;
                }

            _player = FindObjectOfType<Character.Character>();
        }

        // Update is called once per frame
        private void Update() {
            UpdateHudInformation();
        }

        private void UpdateHudInformation() {
            _health.value = _player.Health;
            Points.text = _player.Points.ToString();
            CurrentLevel.text = "Level " + GameManager.Instance.Level;
        }
    }
}