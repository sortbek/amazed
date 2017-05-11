using System;
using System.Net.Mime;
using System.Net.NetworkInformation;
using Assets.Scripts.HUD;
using Assets.Scripts.Items.Potions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.World {
    public class GameManager : Singleton<GameManager> {
        public string GameSeed;
        public int Size;
        public bool RandomSeed;
        public bool Debug;

        public Character.Character Character;
        public StatsUpdater StatsUpdater;
        public PotionSelection PotionSelection;


        private System.Random _random;

        public int GetRandom(int min = 0, int max = 0) {
            if (_random == null) {
                _random = new System.Random(GameSeed.GetHashCode());
            }
            if (min == 0 & max == 0) {
                return _random.Next();
            }
            return max == 0 ? _random.Next(min) : _random.Next(min, max);
        }

        public void LoadNextLevel() {
            Save();
            SceneManager.LoadScene(2);
        }

        public void SetGameObjects() {
            Character = FindObjectOfType<Character.Character>();
            StatsUpdater = FindObjectOfType<StatsUpdater>();
            PotionSelection = FindObjectOfType<PotionSelection>();
        }

        public void Save() {
            SetGameObjects();
            PlayerPrefs.SetInt("ha", PotionSelection.Health.Amount);
            PlayerPrefs.SetInt("hra", PotionSelection.HealthRegeneration.Amount);
            PlayerPrefs.SetInt("dama", PotionSelection.Damage.Amount);
            PlayerPrefs.SetInt("defa", PotionSelection.Defense.Amount);
            PlayerPrefs.SetInt("sa", PotionSelection.Speed.Amount);

            TimeSpan timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
            string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            PlayerPrefs.SetString("t", timeText);
        }

        public void Load() {
            SetGameObjects();
            if (PlayerPrefs.HasKey("ha")) PotionSelection.Health.Amount = PlayerPrefs.GetInt("ha", PotionSelection.Health.Amount);
            if (PlayerPrefs.HasKey("hra")) PotionSelection.HealthRegeneration.Amount = PlayerPrefs.GetInt("hra", PotionSelection.HealthRegeneration.Amount);
            if (PlayerPrefs.HasKey("dama")) PotionSelection.Damage.Amount = PlayerPrefs.GetInt("dama", PotionSelection.Damage.Amount);
            if (PlayerPrefs.HasKey("defa")) PotionSelection.Defense.Amount = PlayerPrefs.GetInt("defa", PotionSelection.Defense.Amount);
            if (PlayerPrefs.HasKey("sa")) PotionSelection.Speed.Amount = PlayerPrefs.GetInt("sa", PotionSelection.Speed.Amount);

            Character.transform.position = new Vector3(0, 1, -12);
            Character.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        void OnApplicationQuit() {
            PlayerPrefs.DeleteAll();
        }
    }
}
