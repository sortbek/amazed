using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class DamagePotion : Potion {
        public DamagePotion() {
            Texture = (Texture) Resources.Load("Sprites/potion_damage", typeof(Texture));
        }

        public override void Use() {
            // Increase damage for a limited time
            Amount -= 1;
        }
    }
}