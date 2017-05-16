using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAnimation : MonoBehaviour {
    public void StartLevel() {
        SceneManager.LoadScene(1);
    }
}
