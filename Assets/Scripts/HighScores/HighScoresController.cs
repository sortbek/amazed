using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HighScores {
    public class HighScoresController {
        public HighScores HighScores;

        public HighScoresController() { 
            HighScores = new HighScores();
        }

        public void SaveHighScores(string name, int level, int points) {
            HighScores = LoadHighScores();
            if(HighScores == null) HighScores = new HighScores();
            HighScores.HighScoresList.Add(new HighScore {
                Name = name,
                Points = points,
                Level = level
            });

            HighScores.HighScoresList = HighScores.HighScoresList.OrderBy(score => score.Points).ToList();
            if(HighScores.HighScoresList.Count > 5) HighScores.HighScoresList.RemoveRange(0, 1);
            File.WriteAllText(Application.persistentDataPath + "/highscores.json",
                JsonUtility.ToJson(HighScores, true));
        }

        public HighScores LoadHighScores() {
            try {
                return JsonUtility.FromJson<HighScores>(
                    File.ReadAllText(Application.persistentDataPath + "/highscores.json"));
            }
            catch (FileNotFoundException e) {
                return new HighScores();
            }
        }
    }
}