using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD
{
    public class StatsUpdater : MonoBehaviour
    {
        private Slider _health;
        private Text _points;
        private Character.Character _player;

        // Use this for initialization
        void Start()
        {
            _health = GetComponentInChildren<Slider>();

            foreach (var text in GetComponentsInChildren<Text>())
                if (text.name == "PointsAmount")
                {
                    _points = text;
                    break;
                }

            _player = FindObjectOfType<Character.Character>();
            _player.Health = 50.0f;

        }

        // Update is called once per frame
        void Update() {
            UpdateHudInformation();
        }

        void UpdateHudInformation()
        {
            _health.value = _player.Health;
            _points.text = Math.Round(_player.Health).ToString();
        }
    }
}