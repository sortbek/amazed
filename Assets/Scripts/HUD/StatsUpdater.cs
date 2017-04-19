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
        }

        // Update is called once per frame
        void Update()
        {
            if (_player.Health > 0) _player.Health -= 0.1f;
            UpdateHudInformation();
        }

        void UpdateHudInformation()
        {
            _health.value = _player.Health;
            _points.text = Math.Round(_player.Health).ToString();
        }
    }
}