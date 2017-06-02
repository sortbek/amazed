using System.Collections;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

// Created By:
// Niek van den Brink
// S1078937
namespace Interaction {
    public class InteractionBehaviour : MonoBehaviour {
        internal Text Interaction;
        public string Name;

        public CharacterPotionController PotionController;

        protected bool ShowEventLog;
        public CharacterWeaponController WeaponController;

        // Use this for initialization
        protected virtual void Start() {
            ShowEventLog = false;

            Interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();

            PotionController = FindObjectOfType<CharacterPotionController>();
            WeaponController = FindObjectOfType<CharacterWeaponController>();

            Interaction.text = "";
        }

        protected virtual void Interact(Character actor) { }

        public virtual void PossibleInteraction(Character actor) { }

        protected IEnumerator ClearInteractionWait() {
            ShowEventLog = true;
            var txt = Interaction.text;
            yield return new WaitForSeconds(2);
            if (Interaction.text == txt) Interaction.text = "";
            ShowEventLog = false;
        }

        protected void ClearInteraction() {
            if (ShowEventLog) return;

            Interaction.text = "";
        }
    }
}