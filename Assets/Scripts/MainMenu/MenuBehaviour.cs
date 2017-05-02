﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    public Canvas QuitMenu;
    public Canvas SettingMenu;
    public Canvas MainMenu;
    public Button StartText;
    public Button ExitText;
    public Button SettingText;

    void Start() {
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

    public void BackToMenuPress() {
        EnableMainMenu();
    }

    public void StartLevel() {
        SceneManager.LoadScene(1); //this will load our first level from our build settings. "1" is the second scene in our game
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