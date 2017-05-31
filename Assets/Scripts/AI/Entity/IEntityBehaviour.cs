using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public interface IEntityBehaviour {

        Vector3 Update(LivingEntity entity);
    }
}
