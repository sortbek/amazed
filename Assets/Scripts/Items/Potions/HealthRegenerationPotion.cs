using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    internal class HealthRegenerationPotion : Potion {
        public HealthRegenerationPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_regen", typeof(Texture));
            Boost = 4;
            Duration = 10;
            Amount = 1;
        }
    }
}