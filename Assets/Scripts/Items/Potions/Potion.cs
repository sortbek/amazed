using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    public abstract class Potion {
        public bool Active;
        public int Amount;
        public float Boost;
        public int Duration;
        public Character.Character Player;
        public Texture Texture;
        public int TimeLeft;

        protected Potion(Character.Character player) {
            Amount = 5;
            Player = player;
            Duration = 20;
        }

        public virtual void Use() {
            TimeLeft = Duration;

            var pr = new PotionRunnable(Player, this);
            pr.Start();

            Amount -= 1;
            Active = true;
        }

        public virtual void RemoveEffect() {
            Active = false;
        }
    }
}