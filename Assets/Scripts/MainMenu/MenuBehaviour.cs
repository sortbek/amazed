using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created By:
// Jordi Wolthuis
// S1085303

public class MenuBehaviour : MonoBehaviour {
    public Button ExitText;
    public Canvas MainMenu;

    public Canvas QuitMenu;
    public Canvas SettingMenu;
    public Button SettingText;
    public Button StartText;

    private void Start() {
        MainMenu = MainMenu.GetComponent<Canvas>();
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        SettingMenu = SettingMenu.GetComponent<Canvas>();
        StartText = StartText.GetComponent<Button>();
        ExitText = ExitText.GetComponent<Button>();
        SettingText = SettingText.GetComponent<Button>();
        QuitMenu.enabled = false;
        SettingMenu.enabled = false;
    }

    public void ExitPress() {
        EnableExitMenu();
    }

    public void SettingPress() {
        EnableSettingsMenu();
    }

    public void HighScoresPress() {
        SceneManager.LoadScene(3);
    }

    public void BackToMenuPress() {
        EnableMainMenu();
    }

    public void StartLevel() {
        SceneManager
            .LoadScene(1); //this will load our first level from our build settings. "1" is the second scene in our game
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void EnableMainMenu() {
        MainMenu.enabled = true;
        StartText.enabled = true;
        ExitText.enabled = true;
        SettingText.enabled = true;
        SettingMenu.enabled = false;
        QuitMenu.enabled = false;
    }

    public void EnableSettingsMenu() {
        SettingMenu.enabled = true;
        MainMenu.enabled = false;
        QuitMenu.enabled = false;
    }

    public void EnableExitMenu() {
        QuitMenu.enabled = true;
        SettingMenu.enabled = false;
        MainMenu.enabled = false;
    }
}