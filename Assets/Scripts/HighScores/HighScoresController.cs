using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.HighScores {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class HighScoresController {
        public HighScores HighScores;

        public HighScoresController() {
            HighScores = new HighScores();
        }

        public void SaveHighScores(string name, int level, int points) {
            HighScores = LoadHighScores() ?? new HighScores();
            if (HighScores.HighScoresList.Count > 4 && HighScores.HighScoresList[4].Points > points) return;

            HighScores.HighScoresList.Add(new HighScore {
                Name = name,
                Points = points,
                Level = level
            });

            HighScores.HighScoresList = HighScores.HighScoresList.OrderByDescending(score => score.Points).ToList();
            if (HighScores.HighScoresList.Count > 5) HighScores.HighScoresList.RemoveAt(5);
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