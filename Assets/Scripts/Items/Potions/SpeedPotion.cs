using UnityEngine;

namespace Assets.Scripts.Items.Potions
{
    class SpeedPotion : Potion
    {
        public SpeedPotion()
        {
            Texture = (Texture)Resources.Load("Sprites/potion_speed", typeof(Texture));
        }
        public override void Use()
        {
            // Increase speed for a limited time
            Amount -= 1;
        }
    }
}
