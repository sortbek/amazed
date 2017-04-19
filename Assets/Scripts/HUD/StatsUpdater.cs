using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUD {
    public class StatsUpdater : MonoBehaviour {
        private float i;
        private Text health;
        private Text stamina;

        // Use this for initialization
        void Start() {
            foreach (var text in GetComponentsInChildren<Text>()) {
                switch (text.name) {
                    case "health":
                        health = text;
                        break;
                    case "stamina":
                        stamina = text;
                        break;
                }
            }
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f) i += Input.GetAxis("Mouse ScrollWheel") > 0 ? 1 : -1;
            UpdateHUDInformation();
        }

        void UpdateHUDInformation() {
            health.text = "Health: " + i;
            stamina.text = "Stamina: " + i;
        }
    }
}