using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    public abstract class Potion {
        public int Amount;
        public Texture Texture;
        public Character.Character Player;
        public float Boost;
        public bool Active;
        public int Duration;
        public int TimeLeft;

        protected Potion(Character.Character player) {
            Amount = 0;
            Player = player;
            Duration = 20;
        }

        public virtual void Use() {
            TimeLeft = Duration;

            PotionRunnable pr = new PotionRunnable(Player, this);
            pr.Start();

            Amount -= 1;
            Active = true;
        }

        public virtual void RemoveEffect() {
            Active = false;
        }
    }
}