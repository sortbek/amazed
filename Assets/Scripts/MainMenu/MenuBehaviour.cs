using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    public Canvas quitMenu;
    public Canvas settingMenu;
    public Canvas mainMenu;
    public Button startText;
    public Button exitText;
    public Button settingText;

    void Start()
    {
        mainMenu = mainMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        settingMenu = settingMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        settingText = settingText.GetComponent<Button>();
        quitMenu.enabled = false;
        settingMenu.enabled = false;
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
        EnableMainMenu();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1); //this will load our first level from our build settings. "1" is the second scene in our game

    }

    public void ExitGame()
    {
        Application.Quit();

    }

    public void EnableMainMenu()
    {
        mainMenu.enabled = true;
        startText.enabled = true;
        exitText.enabled = true;
        settingText.enabled = true;
        settingMenu.enabled = false;
        quitMenu.enabled = false;
    }

    public void EnableSettingsMenu()
    {
        settingMenu.enabled = true;
        mainMenu.enabled = false;
        quitMenu.enabled = false;
    }

    public void EnableExitMenu()
    {
        quitMenu.enabled = true;
        settingMenu.enabled = false;
        mainMenu.enabled = false;
    }

}
