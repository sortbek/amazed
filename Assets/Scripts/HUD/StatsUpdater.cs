using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    public class StatsUpdater : MonoBehaviour {
        private float i = 0.0f;
        private Slider health;
        private Text points;

        // Use this for initialization
        void Start() {
            health = GetComponentInChildren<Slider>();

            foreach (var text in GetComponentsInChildren<Text>())
                if (text.name == "PointsAmount") {
                    points = text;
                    break;
                }
        }

        // Update is called once per frame
        void Update() {
            if(i < 100) i += 0.3f;
            UpdateHUDInformation();
        }

        void UpdateHUDInformation() {
            health.value = i;
            points.text = Math.Round(i * 100).ToString();
        }
    }
}