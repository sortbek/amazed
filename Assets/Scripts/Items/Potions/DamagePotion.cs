using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class DamagePotion : Potion {
        public DamagePotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_damage", typeof(Texture));
            Boost = 20;
        }

        public override void Use() {
            Player.ATT += Boost;
            base.Use();
        }

        public override void RemoveEffect() {
            Player.ATT -= Boost;
            base.RemoveEffect();
        }
    }
}