using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class CharacterInteraction {
    private float _interactionRadius;
    private readonly Character _character;
    private int _layerMask, _propLayer, _playerLayer;

    public CharacterInteraction(Character character) {
        _propLayer = LayerMask.NameToLayer("Prop");
        _playerLayer = LayerMask.NameToLayer("Player");
        var PropLayerMask = 1 << _propLayer;
        var PlayerLayerMask = 1 << _playerLayer;
        _layerMask = PropLayerMask | PlayerLayerMask;

        _character = character;
        _interactionRadius = 2.0f;
    }

    // Update is called once per frame
    public void Update() {
        Collider[] nearbyColliders = Physics.OverlapSphere(
            _character.transform.position,
            _interactionRadius,
            _layerMask
        );

        var Interactables = new List<InteractionBehaviour>();

        foreach (var c in nearbyColliders) {
            if (c.gameObject.layer != _propLayer) {
                continue;
            }

            var interactable = (c.gameObject.GetComponent("InteractionBehaviour") as InteractionBehaviour);
            if (interactable != null) {
                if (interactable.HasBeenInteractedWith) {
                    continue;
                }

                Interactables.Add(interactable);
            }
        }

        var closest = Interactables.FirstOrDefault();
        if (closest != null) {
            closest.PossibleInteraction();
            if (Input.GetKeyDown(KeyCode.E)) {
                closest.Interact();
            }
        }
        else {
            var interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
            interaction.text = "";
        }

        // Check if prop is nearby (withtin x distance)
        // https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
        // Check if gameobjects of layermask 'Props' are within the sphere

        // If multiple prop are nearby sort by where the camera is looking at most

        // Show on screen 'Search {propname}'

        // If prop has not been interacted with continue
        // If 'E' is pressed try to interact with the prop
        // If inventory is full drop gathered item on the ground
        // Else put item in inventory
        // Notify user what happened on screen:
        // '{itemname} found'
        // If inventory full also: 'Inventory is full, the {itemname} is dropped on the ground'
    }
}