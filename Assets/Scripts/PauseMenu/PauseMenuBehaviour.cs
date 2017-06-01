using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour {
    public Canvas uiCanvas;

    public Canvas PauseMenu;
    public Text title;

    public Canvas MenuCanvas;
    public Button Resume;
    public Button Settings;
    public Button Exit;

    public Canvas QuitMenu;

    public Canvas SettingsMenu;


    void Start() {
        uiCanvas = uiCanvas.GetComponent<Canvas>();
        title = title.GetComponent<Text>();

        PauseMenu = PauseMenu.GetComponent<Canvas>();

        MenuCanvas = MenuCanvas.GetComponent<Canvas>();
        Resume = Resume.GetComponent<Button>();
        Settings = Settings.GetComponent<Button>();
        Exit = Exit.GetComponent<Button>();

        QuitMenu = QuitMenu.GetComponent<Canvas>();

        SettingsMenu = SettingsMenu.GetComponent<Canvas>();
        disableAll();
        title.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            pause();
        }
    }

    public void pause() {
        Debug.Log(Time.timeScale);
        if (Time.timeScale == 1) {
            MenuCanvas.gameObject.SetActive(true);
            title.enabled = true;
            EnablePauseMenu();
            Cursor.lockState = CursorLockMode.None;
            uiCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            disableAll();
            title.enabled = false;
            uiCanvas.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    private void disableAll() {
        DisableExitMenu();
        DisablePauseMeu();
        DisableSettingsMenu();
    }

    public void EnablePauseMenu() {
        disableAll();
        MenuCanvas.gameObject.SetActive(true);
        uiCanvas.gameObject.SetActive(false);
    }

    public void EnableSettingsMenu() {
        disableAll();
        SettingsMenu.gameObject.SetActive(true);
        uiCanvas.gameObject.SetActive(false);
    }

    public void EnableExitMenu() {
        disableAll();
        QuitMenu.gameObject.SetActive(true);
        uiCanvas.gameObject.SetActive(false);
    }

    public void DisablePauseMeu() {
        MenuCanvas.gameObject.SetActive(false);
        uiCanvas.gameObject.SetActive(true);
    }

    public void DisableSettingsMenu() {
        SettingsMenu.gameObject.SetActive(false);
    }

    public void DisableExitMenu() {
        QuitMenu.gameObject.SetActive(false);
    }

    public void ExitGame() {
        SceneManager.LoadScene(0);
    }

    public void ExitPress() {
        EnableExitMenu();
    }

    public void SettingPress() {
        EnableSettingsMenu();
    }

    public void BackToMenuPress() {
        EnablePauseMenu();
    }

    public void ResumeLevel() {
        if (Time.timeScale == 0) {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
            disableAll();
            title.enabled = false;
        }
    }
}