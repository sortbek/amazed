using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    internal class DefensePotion : Potion {
        public double NextActionTime = 30.0f;

        public DefensePotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_defense", typeof(Texture));
            Boost = 20;
        }

        public override void Use() {
            Player.DEF += Boost;
            base.Use();
        }

        public override void RemoveEffect() {
            Player.DEF -= Boost;
            base.RemoveEffect();
        }
    }
}