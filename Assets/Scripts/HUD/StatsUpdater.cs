using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    public class StatsUpdater : MonoBehaviour {
        private Text health;
        private Text points;

        // Use this for initialization
        void Start() {
            foreach (var text in GetComponentsInChildren<Text>()) {
                switch (text.name) {
                    case "Health":
                        health = text;
                        break;
                    case "Points":
                        points = text;
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update() {
            UpdateHUDInformation();
        }

        void UpdateHUDInformation() {
            health.text = "Health: ";
            points.text = "Points: ";
        }
    }
}