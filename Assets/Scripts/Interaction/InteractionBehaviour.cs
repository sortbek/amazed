using System.Collections;
using Assets.Scripts.Character;
using Items;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBehaviour : MonoBehaviour {
    public bool HasBeenInteractedWith;
    public string Name;

    // TODO: Replace Item(string) with Item(ItemClass)
    public string Item;

    private Text _interaction;
    private Text _eventlog;

    // Use this for initialization
    private void Start() {
        _interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
        _eventlog = GameObject.FindGameObjectWithTag("eventlog").GetComponent<Text>();

        _interaction.text = "";
        _eventlog.text = "";

        HasBeenInteractedWith = false;
        Item = LootDropTableManager.GetRandomLoot(LootDropTableManager.Default);

        // print(string.Format("{0} in {1}", Item, Name));
    }

    private void Interact(Character actor) {
        HasBeenInteractedWith = true;
        _interaction.text = "";
        _eventlog.text = string.Format("{0} found in {1}", Item, Name);

        // Clear event log after 2 seconds
        StartCoroutine(ClearEventLog());

        // TODO: Add item to actor's inventory
    }

    private IEnumerator ClearEventLog() {
        yield return new WaitForSeconds(2);
        _eventlog.text = "";
    }

    public void PossibleInteraction(Character actor) {
        if (_interaction == null || HasBeenInteractedWith) return;

        _interaction.text = string.Format("Press 'F' to search {0}", Name);

        if (Input.GetKeyDown(KeyCode.F)) {
            Interact(actor);
        }
    }
}