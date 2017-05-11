using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBehaviour : MonoBehaviour {
    private Text _interaction;
    public bool HasBeenInteractedWith;

    // Use this for initialization
    void Start() {
        HasBeenInteractedWith = false;
        _interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
        // Radomly decide what item this prop holds
    }

    // Update is called once per frame
    void Update() {
    }

    public void Interact() {
        print("Im being interacted with :o");
        HasBeenInteractedWith = true;
        _interaction.text = "";
    }

    public void PossibleInteraction() {
        if (_interaction != null && !HasBeenInteractedWith) {
            _interaction.text = String.Format("Press 'E' to search {0}", ToString());
        }
    }
}