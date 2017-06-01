using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public abstract class AbstractEntityBehaviour {
        protected LivingEntity Entity;

        public AbstractEntityBehaviour(LivingEntity entity) {
            Entity = entity;
            RotationSpeed = 10f;
        }

        public abstract Vector3 Update();

        public float RotationSpeed { get; private set; }

        public void Rotate(LivingEntity entity, Vector3 dir) {
            entity.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(entity.transform.forward, dir - entity.transform.position,
                Time.deltaTime * RotationSpeed, 0.0f));
        }
    }
}
