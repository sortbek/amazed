using Assets.Scripts.Character;
using Assets.Scripts.Util;
using Items;
using UnityEngine;
using Util;

namespace Interaction {
    public class SearchInteraction : InteractionBehaviour {
        public bool HasBeenInteractedWith = false;
        public Item Item;

        protected override void Start() {
            base.Start();
            Item = LootDropTableManager.GetRandomLoot(LootDropTableManager.PotionDropTable);
        }

        protected override void Interact(Character actor) {
            HasBeenInteractedWith = true;
            Eventlog.text = string.Format("{0} found in {1}", ItemUtil.ItemToString(Item), Name);

            base.Interact(actor);

            PotionController.Add(Item);
            WeaponController.Add(Item);
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