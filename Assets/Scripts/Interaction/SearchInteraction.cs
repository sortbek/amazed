using Assets.Scripts.Character;
using Items;
using UnityEngine;

namespace Interaction {
    public class SearchInteraction : InteractionBehaviour {
        public bool HasBeenInteractedWith = false;
        public string Item;

        protected override void Start() {
            base.Start();
            Item = LootDropTableManager.GetRandomLoot(LootDropTableManager.Default);
        }

        protected override void Interact(Character actor) {
            HasBeenInteractedWith = true;
            Eventlog.text = string.Format("{0} found in {1}", Item, Name);

            base.Interact(actor);

            PotionController.Add(Item);
        }

        public override void PossibleInteraction(Character actor) {
            base.PossibleInteraction(actor);

            if (Interaction == null || HasBeenInteractedWith) return;

            Interaction.text = string.Format("Press 'F' to search {0}", Name);

            if (Input.GetKeyDown(KeyCode.F)) {
                Interact(actor);
            }
        }
    }
}