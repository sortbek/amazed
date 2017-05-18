using UnityEngine;

namespace Assets.Scripts.Items.Potions {
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
            Player.Speed -= Boost;
            base.RemoveEffect();
        }
    }
}