using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD
{
    public class StatsUpdater : MonoBehaviour
    {
        private Slider _health;
        public Text Points;
        private Character.Character _player;

        // Use this for initialization
        void Start()
        {
            _health = GetComponentInChildren<Slider>();

            foreach (var text in GetComponentsInChildren<Text>())
                if (text.name == "PointsAmount")
                {
                    Points = text;
                    break;
                }

            _player = FindObjectOfType<Character.Character>();
        }

        // Update is called once per frame
        void Update() {
            UpdateHudInformation();
        }

        void UpdateHudInformation()
        {
            _health.value = _player.Health;
            Points.text = _player.Points.ToString();
        }
    }
}