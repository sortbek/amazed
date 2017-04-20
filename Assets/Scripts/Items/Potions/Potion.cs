using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    public abstract class Potion {
        public int Amount;
        public Texture Texture;
        public Character.Character Player;
        public int Boost;
        public bool Active;
        public int TimeLeft;

        protected Potion(Character.Character player) {
            Amount = 5;
            Player = player;
            TimeLeft = 20;
        }

        public virtual void Use() {
            PotionRunnable pr = new PotionRunnable(Player, this);
            pr.Start();

            Amount -= 1;
            Active = true;
        }

        public virtual void RemoveEffect() {
            TimeLeft = 20;
            Active = false;
        }
    }
}