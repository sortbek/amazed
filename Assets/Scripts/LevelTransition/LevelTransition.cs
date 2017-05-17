using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LevelTransition {
    public class LevelTransition : MonoBehaviour {
        private Text _summaryText, _timeAmount, _pointsAmount, _pointsTotalAmount;
        private Character.Character _character;

        // Use this for initialization
        void Start() {
            Cursor.lockState = CursorLockMode.None;

            _character = FindObjectOfType<Character.Character>();
            int levelPoints = 1000 - (int)PlayerPrefs.GetFloat("sec");
            _character.Points += levelPoints;

            _character.transform.position = new Vector3(10, 2, 10);
            _character.transform.rotation = Quaternion.Euler(0, 0, 0);

            foreach (var text in GetComponentsInChildren<Text>()) {
                switch (text.name) {
                    case "SummaryText":
                        _summaryText = text;
                        break;
                    case "TimeAmount":
                        _timeAmount = text;
                        break;
                    case "PointsAmount":
                        _pointsAmount = text;
                        break;
                    case "PointsTotalAmount":
                        _pointsTotalAmount = text;
                        break;
                }
            }

            _summaryText.text = "Level " + GameManager.Instance.Level + " summary";
            _timeAmount.text = PlayerPrefs.GetString("t");
            _pointsAmount.text = levelPoints.ToString();
            _pointsTotalAmount.text = "Total points: " + _character.Points;

            GameManager.Instance.Level += 1;
            GameManager.Instance.Size += GameManager.Instance.Level;
        }
    }
}
