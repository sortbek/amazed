using System.Linq;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.HighScores {

    // Created by:
    // Hugo Kamps
    // S1084074
    public class HighScoresUpdater : MonoBehaviour {
        private GameObject[] _names, _points, _levels;
        private HighScoresController _highScoresController;

        // Use this for initialization
        void Start() {
            _names = GameObject.FindGameObjectsWithTag("hs_name").OrderByDescending(o => o.transform.position.y).ToArray();
            _points = GameObject.FindGameObjectsWithTag("hs_points").OrderByDescending(o => o.transform.position.y).ToArray();
            _levels = GameObject.FindGameObjectsWithTag("hs_level").OrderByDescending(o => o.transform.position.y).ToArray();

            _highScoresController = GameManager.Instance.GetHighScoresController();

            FillTextComponents();
        }

        private void FillTextComponents() {
            for (var index = 0; index < _highScoresController.LoadHighScores().HighScoresList.Count; index++) {
                var highScore = _highScoresController.LoadHighScores().HighScoresList[index];
                _names[index].GetComponent<Text>().text = highScore.Name;
                _points[index].GetComponent<Text>().text = highScore.Points.ToString();
                _levels[index].GetComponent<Text>().text = highScore.Level.ToString();
            }
        }

        public void BackToMainMenu() {
            SceneManager.LoadScene(0);
        }
    }
}
