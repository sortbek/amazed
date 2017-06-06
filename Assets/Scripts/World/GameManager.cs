using System;
using Assets.Scripts.HighScores;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Assets.Scripts.World {
    // Created by:
    // Jeffrey Wienen
    // S1079065 
    public class GameManager : Singleton<GameManager> {
        private Random _random;

        public Character.Character Character;
        public bool Debug;

        public Transform EndPoint;
        public string GameSeed;

        public HighScoresController HighScoresController;

        public int Level = 1;
        public int Size = 5;

        public int GetRandom(int min = 0, int max = 0) {
            if (_random == null) _random = new Random(GameSeed.GetHashCode());
            if ((min == 0) & (max == 0)) return _random.Next();
            return max == 0 ? _random.Next(min) : _random.Next(min, max);
        }

        // Created by:
        // Hugo Kamps
        // S1084074
        // Opens the Level Transition scene to go to the next level
        public void LoadNextLevel() {
            Save();
            SceneManager.LoadScene(2);
        }

        // Created by:
        // Hugo Kamps
        // S1084074
        // Gets the needed GameObjects needed for the transition to new levels 
        public void SetGameObjects() {
            if (Character == null) Character = FindObjectOfType<Character.Character>();
        }

        // Created by:
        // Hugo Kamps
        // S1084074
        public HighScoresController GetHighScoresController() {
            return HighScoresController ?? (HighScoresController = new HighScoresController());
        }

        // Created by:
        // Hugo Kamps
        // S1084074
        // Saves the needed data about the game on the end of a level
        public void Save() {
            SetGameObjects();
            var elapsedSeconds = Time.timeSinceLevelLoad;
            var timeSpan = TimeSpan.FromSeconds(elapsedSeconds);
            var timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            PlayerPrefs.SetFloat("sec", elapsedSeconds);
            PlayerPrefs.SetString("t", timeText);
        }

        // Created by:
        // Hugo Kamps
        // S1084074
        // On the start of each new level place the character in the correct start position
        public void Load() {
            SetGameObjects();

            Character.gameObject.SetActive(true);
            Character.transform.position = GetStartPoint() - new Vector3(0, 0, 12);
        }

        // Created by:
        // Hugo Kamps
        // S1084074
        // Deletes the temporary save game data once the application quits
        private void OnApplicationQuit() {
            PlayerPrefs.DeleteAll();
        }

        public Vector3 GetEndpoint() {
            var offset = Size * 12 / 2 - 6;
            return new Vector3((Size - 1) * 12 - offset, 0, Size * 12 - offset);
        }

        public Vector3 GetStartPoint() {
            var offset = Size * 12 / 2 - 6;
            return new Vector3(-offset, 2, -offset);
        }
    }
}