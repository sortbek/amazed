using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    internal class HealthPotion : Potion {
        public HealthPotion(Character.Character character) : base(character) {
            Texture = (Texture) Resources.Load("Sprites/potion_health", typeof(Texture));
            Boost = 30;
            Duration = 0;
            Amount = 2;
        }

        public override void Use() {
            if (Character.Health > Scripts.Character.Character.MaxHealth - Boost)
                Character.Health = Scripts.Character.Character.MaxHealth;
            else Character.Health += Boost;
            base.Use();
        }
    }
}