using System.Collections.Generic;
using Assets.Scripts.Character;
using Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            _interactionRadius = 3.5f;
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

                // This will only be the case with the Player GameObject
                if (c.gameObject.layer != _propLayer) continue;

                // Try to get Interaction behaviour of found prop
                var interactable = (c.gameObject.GetComponent("InteractionBehaviour") as InteractionBehaviour);

                // Filter out non interactable props
                if (interactable == null) continue;

                interactables.Add(interactable);
            }

            var closest = GetClosestProp(interactables);

            // Show interaction text if there is a closest Prop
            if (closest != null) {
                closest.PossibleInteraction(_character);
            }
            else {
                // No props were found => Interaction text is cleared
                if (SceneManager.GetActiveScene().name == "Game") {
                    GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>().text = "";
                    GameObject.FindGameObjectWithTag("eventlog").GetComponent<Text>().text = "";
                }
            }
        }

        private InteractionBehaviour GetClosestProp(IEnumerable<InteractionBehaviour> props) {
            InteractionBehaviour pMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = _character.transform.position;

            foreach (var p in props) {
                // Foreach prop check the distance to the player
                var dist = Vector3.Distance(p.transform.position, currentPos);

                // Only save when the newest distance is the shortest
                if (!(dist < minDist)) continue;

                pMin = p;
                minDist = dist;
            }

            return pMin;
        }
    }
}