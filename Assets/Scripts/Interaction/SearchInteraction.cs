using Assets.Scripts.Character;
using Items;
using UnityEngine;
using Util;

// Created By:
// Niek van den Brink
// S1078937
namespace Interaction {
    public class SearchInteraction : InteractionBehaviour {
        public bool HasBeenInteractedWith = false;
        public LootTable LootTable = LootTable.Potions;
        public float DropChance = 0.5f;

        private Item _item;

        protected override void Start() {
            base.Start();
            _item = LootTableManager.GetRandomLoot(LootTable, DropChance);
        }

        protected override void Interact(Character actor) {
            Interaction.text = string.Format("{0} Found", ItemUtil.ItemToString(_item));

            PotionController.Add(_item);
            WeaponController.Add(_item);

            StartCoroutine(ClearInteractionWait());
            HasBeenInteractedWith = true;
        }

        public override void PossibleInteraction(Character actor) {
            ClearInteraction();

            if (Interaction == null || HasBeenInteractedWith) return;

            if (ShowEventLog) return;
            Interaction.text = string.Format("[F] Search {0}", Name);

            if (Input.GetKeyDown(KeyCode.F)) {
                Interact(actor);
            }
        }
    }
}