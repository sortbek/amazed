using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class HealthRegenerationPotion : Potion {
        public HealthRegenerationPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_regen", typeof(Texture));
            Boost = 2;
        }

        public override void Use() {
            PotionRunnable pr = new PotionRunnable(Player, this, 4000);
            pr.Start();

            base.Use();
        }
    }
}