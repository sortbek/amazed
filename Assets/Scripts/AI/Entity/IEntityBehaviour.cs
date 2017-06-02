using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public interface IEntityBehaviour {
        Vector3 Update(LivingEntity entity);
    }
}