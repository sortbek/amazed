using UnityEngine;

namespace Assets.Scripts.Items.Potions
{
    class HealthRegenerationPotion : Potion
    {
        public HealthRegenerationPotion()
        {
            Texture = (Texture)Resources.Load("Sprites/potion_regen", typeof(Texture));
        }

        public override void Use()
        {
            // Regenerate health for a limited time
            Amount -= 1;
        }
    }
}
