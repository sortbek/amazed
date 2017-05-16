using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class HealthPotion : Potion {
        public HealthPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_health", typeof(Texture));
            Duration = 0;
        }

        public override void Use() {
            if (Player.Health > 70) Player.Health = 100;
            else Player.Health += 30;
            base.Use();
        }
    }
}