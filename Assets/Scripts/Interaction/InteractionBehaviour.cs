using System.Collections;
using Assets.Scripts.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction {
    public class InteractionBehaviour : MonoBehaviour {
        public string Name;

        protected bool ShowEventLog;

        internal Text Interaction;

        public CharacterPotionController PotionController;
        public CharacterWeaponController WeaponController;

        // Use this for initialization
        protected virtual void Start(){
            ShowEventLog = false;
            
            Interaction = GameObject.FindGameObjectWithTag("interaction").GetComponent<Text>();

            PotionController = FindObjectOfType<CharacterPotionController>();
            WeaponController = FindObjectOfType<CharacterWeaponController>();

            Interaction.text = "";
        }

        protected virtual void Interact(Character actor) { }

        public virtual void PossibleInteraction(Character actor) { }

        protected IEnumerator ClearInteractionWait(){
            ShowEventLog = true;
            var txt = Interaction.text;
            yield return new WaitForSeconds(2);
            if (Interaction.text == txt) {
                Interaction.text = "";
            }
            ShowEventLog = false;
        }

        protected void ClearInteraction(){
            if (ShowEventLog) return;
            
            Interaction.text = "";
        }
    }
}