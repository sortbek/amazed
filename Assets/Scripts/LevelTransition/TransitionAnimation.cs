using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This class is used for all the functions that are used during animations
/// </summary>
public class TransitionAnimation : MonoBehaviour {
    
    // Loads a new version of the 'Game' scene
    public void StartLevel() {
        if(GameManager.Instance.Character != null)GameManager.Instance.Character.gameObject.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void HideMenu() {
        foreach (var component in FindObjectsOfType<Canvas>()) {
            component.enabled = false;
        }
    }
}
