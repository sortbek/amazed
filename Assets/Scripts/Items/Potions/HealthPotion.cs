using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class HealthPotion : Potion {
        public HealthPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_health", typeof(Texture));
            Boost = 30;
            Duration = 0;
            Amount = 2;
        }

        public override void Use() {
            if (Player.Health > Character.Character.MAX_HEALTH - Boost) Player.Health = Character.Character.MAX_HEALTH;
            else Player.Health += Boost;
            base.Use();
        }
    }
}