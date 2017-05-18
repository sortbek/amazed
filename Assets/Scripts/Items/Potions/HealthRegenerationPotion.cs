using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    internal class HealthRegenerationPotion : Potion {
        public HealthRegenerationPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_regen", typeof(Texture));
            Boost = 4;
            Duration = 10;
            Amount = 1;
        }
    }
}