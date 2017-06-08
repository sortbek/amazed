using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GameOver {
    // Created by:
    // Hugo Kamps
    // S1084074
    public class GameOverScreen : MonoBehaviour {
        private Character.Character _character;
        private InputField _nameInput;

        private Text _pointsText;
        private Button _saveHighScoresButton;

        // Use this for initialization
        private void Start() {
            Cursor.lockState = CursorLockMode.None;

            foreach (var text in GetComponentsInChildren<Text>())
                switch (text.name) {
                    case "PointsText":
                        _pointsText = text;
                        break;
                }

            _nameInput = GetComponentInChildren<InputField>();
            _saveHighScoresButton = GetComponentsInChildren<Button>()[0];
            _character = GameManager.Instance.Character;
            _character.gameObject.SetActive(false);
            if (_character != null) _pointsText.text += _character.Points.ToString();
        }

        public void SaveHighScoresClick() {
            var name = _nameInput.text;
            if (_character != null && name != "") {
                GameManager.Instance.GetHighScoresController()
                    .SaveHighScores(name, GameManager.Instance.Level, _character.Points);
                _saveHighScoresButton.enabled = false;
                _pointsText.text = "Highscore saved";
            }
        }

        public void BackToMainMenuClick() {
            SceneManager.LoadScene(0);
        }
    }
}