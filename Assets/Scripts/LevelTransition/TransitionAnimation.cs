using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;


// Created by:
// Hugo Kamps
// S1084074
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
