using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    public abstract class Potion {
        public int Amount;
        public Texture Texture;
        public Character.Character Player;
        public bool Active;

        protected Potion(Character.Character player) {
            Amount = 5;
            Player = player;
        }

        public virtual void Use() {
            Amount -= 1;
        }
    }
}