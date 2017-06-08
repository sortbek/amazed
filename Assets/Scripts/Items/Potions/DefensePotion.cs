using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    internal class DefensePotion : Potion {
        public double NextActionTime = 30.0f;

        public DefensePotion(Character.Character character) : base(character) {
            Texture = (Texture) Resources.Load("Sprites/potion_defense", typeof(Texture));
            Boost = 20;
        }

        public override void Use() {
            Character.DEF += Boost;
            base.Use();
        }

        public override void RemoveEffect() {
            Character.DEF -= Boost;
            base.RemoveEffect();
        }
    }
}