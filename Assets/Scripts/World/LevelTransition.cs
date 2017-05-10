using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void NextLevelClick() {
        SceneManager.LoadScene(1);   
    }
}
