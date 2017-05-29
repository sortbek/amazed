using System.Collections;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction {
    public class InteractionBehaviour : MonoBehaviour {
        public string Name;

        internal Text Interaction;
        internal Text Eventlog;

        public CharacterPotionController PotionController;
        public CharacterWeaponController WeaponController;

        // Use this for initialization
        protected virtual void Start() {
            Interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();
            Eventlog = GameObject.FindGameObjectWithTag("eventlog").GetComponent<Text>();

            PotionController = FindObjectOfType<CharacterPotionController>();
            WeaponController = FindObjectOfType<CharacterWeaponController>();

            Interaction.text = "";
            Eventlog.text = "";
        }

        protected virtual void Interact(Character actor) { }

        public virtual void PossibleInteraction(Character actor) { }

        protected void ClearInteraction() {
            Interaction.text = "";
        }

        protected IEnumerator ClearEventLog() {
            var txt = Eventlog.text;
            yield return new WaitForSeconds(2);
            if (Eventlog.text == txt) {
                Eventlog.text = "";
            }
        }
    }
}