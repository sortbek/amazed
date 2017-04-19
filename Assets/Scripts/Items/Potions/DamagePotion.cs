using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class DamagePotion : Potion {

        public DamagePotion(Character.Character player) : base(player) {            
            Texture = (Texture) Resources.Load("Sprites/potion_damage", typeof(Texture));
        }

        public override void Use() {
            Player.ATT += 10;
            Amount -= 1;
        }
    }
}