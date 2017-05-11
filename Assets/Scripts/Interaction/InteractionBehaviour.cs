using System;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBehaviour : MonoBehaviour {
    public bool HasBeenInteractedWith;
    public string Name;
    public String Item;

    private Text _interaction;
    private GameObject _eventlog;

    public InteractionBehaviour(string name) {
        Name = name;
    }

    // Use this for initialization
    void Start() {
        HasBeenInteractedWith = false;
        _interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
        _eventlog = GameObject.FindGameObjectWithTag("eventlog");
        _eventlog.AddComponent<Text>();
        //_eventlog.GetComponentInChildren<Text>().text = "test";
        // Radomly decide what item this prop holds
        Item = LootDropTableManager.GetRandomLoot();
    }

    // Update is called once per frame
    void Update() {
    }

    void Interact(Character actor) {
        //_eventlog.text = String.Format("{0} found in {1}", Item, Name);
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