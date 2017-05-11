using System;
using System.Collections.Generic;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InteractionBehaviour : MonoBehaviour {
    public bool HasBeenInteractedWith;
    public string Name;
    public String Item;

    private Text _interaction;

    public InteractionBehaviour(string name) {
        Name = name;
    }

    // Use this for initialization
    void Start() {
        HasBeenInteractedWith = false;
        _interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
        // Radomly decide what item this prop holds
        Item = LootDropTableManager.GetRandomLoot();
    }

    // Update is called once per frame
    void Update() {
    }

    void Interact(Character actor) {
        print(String.Format("{0} found in {1}", Item, Name));
        HasBeenInteractedWith = true;
        _interaction.text = "";
    }

    public void PossibleInteraction(Character actor) {
        if (_interaction != null && !HasBeenInteractedWith) {
            _interaction.text = String.Format("Press 'F' to search {0}", Name);

            if (Input.GetKeyDown(KeyCode.F)) {
                Interact(actor);
            }
        }
    }
}