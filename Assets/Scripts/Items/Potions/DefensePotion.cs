using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class DefensePotion : Potion {
        public DefensePotion() {
            Texture = (Texture) Resources.Load("Sprites/potion_defense", typeof(Texture));
        }

        public override void Use() {
            // Increase defense for a limited time
            Amount -= 1;
        }
    }
}