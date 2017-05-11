using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBehaviour : MonoBehaviour {
    private Text _interaction;

    // Use this for initialization
    void Start() {
        _interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
        // Radomly decide what item this prop holds
    }

    // Update is called once per frame
    void Update() {
    }

    public void Interact() {
        if (_interaction != null) {
            _interaction.text = String.Format("Press 'E' to search {0}", ToString());
        }
    }
}