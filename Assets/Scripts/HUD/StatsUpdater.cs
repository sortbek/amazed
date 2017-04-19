using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD
{
    public class StatsUpdater : MonoBehaviour
    {
        private Slider health;
        private Text points;
        private Character.Character player;

        // Use this for initialization
        void Start()
        {
            health = GetComponentInChildren<Slider>();

            foreach (var text in GetComponentsInChildren<Text>())
                if (text.name == "PointsAmount")
                {
                    points = text;
                    break;
                }

            player = FindObjectOfType<Character.Character>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player.Health > 0) player.Health -= 0.1f;
            UpdateHUDInformation();
        }

        void UpdateHUDInformation()
        {
            health.value = player.Health;
        }
    }
}