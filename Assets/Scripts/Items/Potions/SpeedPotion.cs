using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    internal class SpeedPotion : Potion {
        public SpeedPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_speed", typeof(Texture));
            Boost = 1.5f;
            Duration = 10;
            Amount = 1;
        }

        public override void Use() {
            Player.Speed *= Boost;
            base.Use();
        }

        public override void RemoveEffect() {
            Player.Speed /= Boost;
            base.RemoveEffect();
        }
    }
}