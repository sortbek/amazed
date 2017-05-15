using System.Collections;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction {
    public class InteractionBehaviour : MonoBehaviour {
        public string Name;

        internal Text Interaction;
        internal Text Eventlog;

        // Use this for initialization
        protected virtual void Start() {
            Interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
            Eventlog = GameObject.FindGameObjectWithTag("eventlog").GetComponent<Text>();

            Interaction.text = "";
            Eventlog.text = "";
        }

        protected virtual void Interact(Character actor) {
            Interaction.text = "";

            // Clear event log after 2 seconds
            StartCoroutine(ClearEventLog());
        }

        private IEnumerator ClearEventLog() {
            yield return new WaitForSeconds(2);
            Eventlog.text = "";
        }

        public virtual void PossibleInteraction(Character actor) {
        }
    }
}