using System;
using Assets.Scripts.HighScores;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    File owner: Jeffrey Wienen
    Created by:
    Jeffrey Wienen     s1079065 
*/

namespace Assets.Scripts.World {
    public class GameManager : Singleton<GameManager> {
        public string GameSeed;
        public int Size = 5;
        public bool Debug;

        public Character.Character Character;

        public int Level = 1;

        // Game stuff
        public Transform EndPoint;
        private System.Random _random;

        public HighScoresController HighScoresController;

        public int GetRandom(int min = 0, int max = 0) {
            if (_random == null) {
                _random = new System.Random(GameSeed.GetHashCode());
            }
            if (min == 0 & max == 0) { 
                return _random.Next();
            }
            return max == 0 ? _random.Next(min) : _random.Next(min, max);
        }

        // Opens the Level Transition scene to go to the next level
        public void LoadNextLevel() {
            Save();
            SceneManager.LoadScene(2);
        }

        // Gets the needed GameObjects needed for the transition to new levels 
        public void SetGameObjects() {
            if (Character == null) Character = FindObjectOfType<Character.Character>();
        }

        public HighScoresController GetHighScoresController() {
            return HighScoresController ?? (HighScoresController = new HighScoresController());
        }

        // Saves the needed data about the game on the end of a level
        public void Save() {
            SetGameObjects();
            var elapsedSeconds = Time.timeSinceLevelLoad;
            var timeSpan = TimeSpan.FromSeconds(elapsedSeconds);
            var timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            PlayerPrefs.SetFloat("sec", elapsedSeconds);
            PlayerPrefs.SetString("t", timeText);
        }

        // On the start of each new level place the character in the correct start position
        public void Load() {
            SetGameObjects();

            Character.gameObject.SetActive(true);
            Character.transform.position = GetStartPoint() - new Vector3(0, 0, 12);

        }

        // Deletes the temporary save game data once the application quits
        void OnApplicationQuit() {
            PlayerPrefs.DeleteAll();
        }

        public Vector3 GetEndpoint() {
            var offset = Size * 12 / 2 - 6;
            return new Vector3((Size - 1) * 12 - offset, 0, Size * 12 - offset);
        }

        public Vector3 GetStartPoint()
        {
            var offset = Size * 12 / 2 - 6;
            return new Vector3(-offset,2, -offset);
        }
    }
}
