using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    public abstract class Potion {
        public int Amount;
        public Texture Texture;
        public Character.Character Player;

        public Potion(Character.Character player)
        {
            Amount = 5;
            Player = player;
        }

        public abstract void Use();
    }
}
