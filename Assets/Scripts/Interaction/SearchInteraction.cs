using Assets.Scripts.Character;
using Items;
using UnityEngine;

namespace Interaction {
    public class SearchInteraction : InteractionBehaviour {
        public bool HasBeenInteractedWith;

        // TODO: Replace Item(string) with Item({ItemClass})
        public string Item;


        protected override void Start() {
            base.Start();

            HasBeenInteractedWith = false;
            Item = LootDropTableManager.GetRandomLoot(LootDropTableManager.Default);

            //print(string.Format("{0} in {1}", Item, Name));
        }

        protected override void Interact(Character actor) {
            HasBeenInteractedWith = true;
            Eventlog.text = string.Format("{0} found in {1}", Item, Name);

            base.Interact(actor);

            // TODO: Add item to actor's inventory
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