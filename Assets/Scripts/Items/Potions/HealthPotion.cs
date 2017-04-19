using UnityEngine;

namespace Assets.Scripts.Items.Potions
{
    class HealthPotion : Potion
    {
        public HealthPotion()
        {
            Texture = (Texture)Resources.Load("Sprites/potion_health", typeof(Texture));
        }

        public override void Use()
        {
            // Increase health
            Amount -= 1;
        }
    }
}
