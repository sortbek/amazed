using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    public abstract class Potion {
        public int Amount;
        public Texture Texture;

        public Potion()
        {
            Amount = 5;
        }

        public abstract void Use();
    }
}
