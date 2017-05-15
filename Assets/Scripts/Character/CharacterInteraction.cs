using System.Collections.Generic;
using Assets.Scripts.Character;
using Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Character {
    public class CharacterInteraction {
        private float _interactionRadius;
        private readonly Character _character;
        private int _layerMask, _propLayer, _playerLayer;


        public CharacterInteraction(Character character) {
            _propLayer = LayerMask.NameToLayer("Prop");
            _playerLayer = LayerMask.NameToLayer("Player");
            var propLayerMask = 1 << _propLayer;
            var playerLayerMask = 1 << _playerLayer;

            // Combine layers Prop and Player
            _layerMask = propLayerMask | playerLayerMask;

            _character = character;
            _interactionRadius = 2.0f;
        }

        // Update is called once per frame
        public void Update() {
            // Checks for Colliders within a sphere, it only checks on the prespecified layers
            var nearbyColliders = Physics.OverlapSphere(
                _character.transform.position,
                _interactionRadius,
                _layerMask
            );

            var interactables = new List<InteractionBehaviour>();

            foreach (var c in nearbyColliders) {
                if (c.gameObject.layer != _propLayer) {
                    // This will only be the case with the Player GameObject
                    continue;
                }

                var interactable = (c.gameObject.GetComponent("InteractionBehaviour") as InteractionBehaviour);

                if (interactable == null) continue;

                interactables.Add(interactable);
            }

            var closest = GetClosestProp(interactables);
            if (closest != null) {
                closest.PossibleInteraction(_character);
            }
            else {
                // No props were found => Interaction text is cleared
                GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>().text = "";
            }

            // Check if prop is nearby (withtin x distance)
            // https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
            // Check if gameobjects of layer 'Props' are within the sphere

            // TODO: If multiple prop are nearby sort by where the camera is looking at most

            // Show on screen 'Search {propname}'

            // If prop has not been interacted with continue
            // If 'F' is pressed try to interact with the prop
            // TODO: If inventory is full drop gathered item on the ground
            // TODO: Else put item in inventory
            // Notify user what happened on screen:
            // '{itemname} found'
            // TODO: If inventory full also: 'Inventory is full, the {itemname} is dropped on the ground'
        }

        private InteractionBehaviour GetClosestProp(IEnumerable<InteractionBehaviour> props) {
            InteractionBehaviour pMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = _character.transform.position;

            foreach (var p in props) {
                var dist = Vector3.Distance(p.transform.position, currentPos);

                if (!(dist < minDist)) continue;

                pMin = p;
                minDist = dist;
            }

            return pMin;
        }
    }
}