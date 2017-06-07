﻿using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public abstract class AbstractEntityBehaviour {

        // Created by:
        // Eelco Eikelboom
        // S1080542
        protected LivingEntity Entity;
        public float RotationSpeed { get; protected set; }

        protected AbstractEntityBehaviour(LivingEntity entity) {
            Entity = entity;
            RotationSpeed = 8f;
        }

        public abstract Vector3 Update();

        public void Rotate(LivingEntity entity, Vector3 dir) {
            entity.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(entity.transform.forward,
                dir - entity.transform.position,
                Time.deltaTime * RotationSpeed, 0.0f));
        }
    }
}