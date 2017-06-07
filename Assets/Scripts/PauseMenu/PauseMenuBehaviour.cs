using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created By:
// Jordi Wolthuis
// S1085303

public class PauseMenuBehaviour : MonoBehaviour {
    public Button Exit;

    public Canvas MenuCanvas;

    public Canvas PauseMenu;

    public Canvas QuitMenu;
    public Button Resume;
    public Button Settings;

    public Canvas SettingsMenu;
    public Text title;
    public Canvas uiCanvas;

    private void Start() {
        uiCanvas = uiCanvas.GetComponent<Canvas>();
        title = title.GetComponent<Text>();

        PauseMenu = PauseMenu.GetComponent<Canvas>();

        MenuCanvas = MenuCanvas.GetComponent<Canvas>();
        Resume = Resume.GetComponent<Button>();
        Settings = Settings.GetComponent<Button>();
        Exit = Exit.GetComponent<Button>();

        QuitMenu = QuitMenu.GetComponent<Canvas>();

        SettingsMenu = SettingsMenu.GetComponent<Canvas>();
        DisableAll();
        title.enabled = false;
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    // setting the timescale wil make the game freeze or unfreeze
    public void Pause() {
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
            DisableAll();
            title.enabled = false;
            uiCanvas.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    private void DisableAll() {
        DisableExitMenu();
        DisablePauseMeu();
        DisableSettingsMenu();
    }

    public void EnablePauseMenu() {
        DisableAll();
        MenuCanvas.gameObject.SetActive(true);
        uiCanvas.gameObject.SetActive(false);
    }

    public void EnableSettingsMenu() {
        DisableAll();
        SettingsMenu.gameObject.SetActive(true);
        uiCanvas.gameObject.SetActive(false);
    }

    public void EnableExitMenu() {
        DisableAll();
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
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
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
            DisableAll();
            title.enabled = false;
        }
    }
}