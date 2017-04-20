using UnityEngine;

namespace Assets.Scripts.Items.Potions {
    class DefensePotion : Potion {
        public double NextActionTime = 30.0f;

        public DefensePotion(Character.Character player) : base(player) {
            Texture = (Texture) Resources.Load("Sprites/potion_defense", typeof(Texture));
            Boost = 20;
        }

        public override void Use() {
            Player.DEF += Boost;

            PotionRunnable pr = new PotionRunnable(Player, this, 4000);
            pr.Start();

            base.Use();
        }

        public override void RemoveEffect() {
            Player.DEF -= Boost;
            base.RemoveEffect();
        }
    }
}