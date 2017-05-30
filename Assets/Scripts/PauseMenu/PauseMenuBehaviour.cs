using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour {

    public Canvas SettingsCanvas;
    public Canvas QuitMenu;
    public Canvas SettingMenu;
    public Canvas MainMenu;
    public Button ResumeText;
    public Button ExitText;
    public Button SettingText;
    public Text title;
    //public Transform player;

    // Use this for initialization
    void Start() {
        //SettingsCanvas.gameObject.SetActive(false);
        MainMenu = MainMenu.GetComponent<Canvas>();
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        SettingMenu = SettingMenu.GetComponent<Canvas>();
        ResumeText = ResumeText.GetComponent<Button>();
        ExitText = ExitText.GetComponent<Button>();
        SettingText = SettingText.GetComponent<Button>();
        disableAll();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            pause();
            Debug.Log("hierbenik ");
        }
    }

    public void pause() {
        Debug.Log(Time.timeScale);
        if (Time.timeScale == 1) {
            Cursor.lockState = CursorLockMode.None;
            //SettingsCanvas.gameObject.SetActive(true);
            print("hierbenik 1");
            EnablePauseMenu();
            Time.timeScale = 0;
          //  player.GetComponent<CharacterController>().enabled = false;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            disableAll();
            //SettingsCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
         //   player.GetComponent<CharacterController>().enabled = true;
        }
     
    }

    private void disableAll()
    {
        DisableExitMenu();
        DisablePauseMeu();
        DisableSettingsMenu();
    }

    public void EnablePauseMenu() {
        title.enabled = true;
        MainMenu.enabled = true;
        ResumeText.enabled = true;
        ExitText.enabled = true;
        SettingText.enabled = true;
        SettingMenu.enabled = false;
        QuitMenu.enabled = false;
    }

    public void EnableSettingsMenu()
    {
        title.enabled = true;
        SettingMenu.enabled = true;
        MainMenu.enabled = false;
        QuitMenu.enabled = false;
    }

    public void EnableExitMenu()
    {
        title.enabled = true;
        QuitMenu.enabled = true;
        SettingMenu.enabled = false;
        MainMenu.enabled = false;
    }

    public void DisablePauseMeu() {
        title.enabled = false;
        MainMenu.enabled = false;
        ResumeText.enabled = false;
        ExitText.enabled = false;
        SettingText.enabled = false;
        SettingMenu.enabled = false;
        QuitMenu.enabled = false;
    }

    public void DisableSettingsMenu()
    {
        title.enabled = false;
        SettingMenu.enabled = false;
        MainMenu.enabled = false;
        QuitMenu.enabled = false;
    }

    public void DisableExitMenu()
    {
        title.enabled = false;
        QuitMenu.enabled = false;
        SettingMenu.enabled = false;
        MainMenu.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitPress()
    {
        EnableExitMenu();
    }

    public void SettingPress()
    {
        EnableSettingsMenu();
    }

    public void BackToMenuPress()
    {
        EnablePauseMenu();
    }

    public void ResumeLevel()
    {
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }

}
