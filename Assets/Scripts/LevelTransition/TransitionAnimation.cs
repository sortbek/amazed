using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.LevelTransition {
    public class TransitionAnimation : MonoBehaviour {
        // Loads a new version of the 'Game' scene
        public void StartLevel() {
            SceneManager.LoadScene(1);
        }
    }
}