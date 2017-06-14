using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    internal class DamagePotion : Potion {
        public DamagePotion(Character.Character character) : base(character) {
            Texture = (Texture) Resources.Load("Sprites/potion_damage", typeof(Texture));
            Boost = 20;
        }

        public override void Use() {
            Character.Att += Boost;
            base.Use();
        }

        public override void RemoveEffect() {
            Character.Att -= Boost;
            base.RemoveEffect();
        }
    }
}