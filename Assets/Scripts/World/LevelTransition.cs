using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Character;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    private Text _pointsAmount;

    // Use this for initialization
    void Start() {
        foreach (var text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "PointsAmount":
                    _pointsAmount = text;
                    break;
            }

        }
        _pointsAmount.text = PlayerPrefs.GetInt("p").ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextLevelClick()
    {
        SceneManager.LoadScene(1);
    }
}
