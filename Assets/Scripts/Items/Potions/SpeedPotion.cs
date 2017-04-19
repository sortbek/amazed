using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class SpeedPotion : Potion {
        public SpeedPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_speed", typeof(Texture));
        }

        public override void Use() {
            Player.Speed += 10;

            base.Use();
        }
    }
}