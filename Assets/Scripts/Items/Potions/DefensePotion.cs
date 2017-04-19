using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class DefensePotion : Potion {
        public double NextActionTime = 30.0f;

        public DefensePotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_defense", typeof(Texture));
        }

        public override void Use() {
            Player.DEF += 10;

            base.Use();
        }
    }
}