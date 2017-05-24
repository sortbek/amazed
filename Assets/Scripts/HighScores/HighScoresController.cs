﻿using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.HighScores {
    public class HighScoresController {
        public HighScores HighScores;

        public HighScoresController() { 
            HighScores = new HighScores();
        }

        public void SaveHighScores(string name, int level, int points) {
            HighScores = LoadHighScores() ?? new HighScores();
            HighScores.HighScoresList.Add(new HighScore {
                Name = name,
                Points = points,
                Level = level
            });

            HighScores.HighScoresList = HighScores.HighScoresList.OrderByDescending(score => score.Points).ToList();
            if(HighScores.HighScoresList.Count > 5) HighScores.HighScoresList.RemoveAt(5);
            File.WriteAllText(Application.persistentDataPath + "/highscores.json",
                JsonUtility.ToJson(HighScores, true));
        }

        public HighScores LoadHighScores() {
            try {
                return JsonUtility.FromJson<HighScores>(
                    File.ReadAllText(Application.persistentDataPath + "/highscores.json"));
            }
            catch (FileNotFoundException) {
                return new HighScores();
            }
        }
    }
}