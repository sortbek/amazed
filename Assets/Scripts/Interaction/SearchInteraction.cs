using System.Collections.Generic;
using Assets.Scripts.Character;
using Items;
using UnityEngine;
using Util;

namespace Interaction {
    public class SearchInteraction : InteractionBehaviour {
        public bool HasBeenInteractedWith = false;
        public LootTable LootTable = LootTable.Potions;

        private Item _item;

        protected override void Start() {
            base.Start();
            _item = LootDropTableManager.GetRandomLoot(LootDropTableManager.GetLootTable(LootTable));
        }

        protected override void Interact(Character actor) {
            Eventlog.text = string.Format("{0} found in {1}", ItemUtil.ItemToString(_item), Name);

            PotionController.Add(_item);
            WeaponController.Add(_item);

            ClearInteraction();
            // Clear event log after 2 seconds
            StartCoroutine(ClearEventLog());
            HasBeenInteractedWith = true;
        }

        public override void PossibleInteraction(Character actor) {
            ClearInteraction();

            if (Interaction == null || HasBeenInteractedWith) return;

            Interaction.color = Color.blue;
            Interaction.text = string.Format("Press 'F' to search {0}", Name);

            if (Input.GetKeyDown(KeyCode.F)) {
                Interact(actor);
            }
        }
    }
}