using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class SpeedPotion : Potion {
        public SpeedPotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_speed", typeof(Texture));
            Boost = 10;
        }

        public override void Use() {
            Player.Speed += Boost;

            PotionRunnable pr = new PotionRunnable(Player, this, 4000);
            pr.Start();

            base.Use();
        }

        public override void RemoveEffect()
        {
            Player.Speed -= Boost;
            base.RemoveEffect();
        }
    }
}