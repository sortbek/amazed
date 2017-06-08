using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    // Created by:
    // Hugo Kamps
    // S1084074
    public abstract class Potion {
        public bool Active { get; set; }
        public int Amount { get; set; }
        public float Boost { get; set; }
        public int Duration { get; set; }
        public Character.Character Character { get; set; }
        public Texture Texture { get; set; }
        public int TimeLeft { get; set; }

        protected Potion(Character.Character character) {
            Amount = 5;
            Character = character;
            Duration = 20;
        }

        public virtual void Use() {
            TimeLeft = Duration;

            var pr = new PotionRunnable(Character, this);
            pr.Start();

            Amount -= 1;
            Active = true;
        }

        public virtual void RemoveEffect() {
            Active = false;
        }
    }
}