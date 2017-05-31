using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public class EntityWanderBehaviour : IEntityBehaviour {

        private readonly float _speed, _triggerDistance, _radius, _rotationSpeed;
        private Vector3? _target;

        public EntityWanderBehaviour() {
            _speed = 1f;
            _rotationSpeed = 10f;
            _radius = 10;
            _triggerDistance = .3f;
            _target = null;
        }

        public Vector3 Update(LivingEntity entity) {
            if (_target == null || Vector3.Distance(entity.transform.position, _target.Value) < _triggerDistance)
                UpdateTarget(entity);
            Rotate(entity, _target.Value);
            return Vector3.MoveTowards(entity.transform.position, _target.Value, _speed* Time.deltaTime);
        }

        private void UpdateTarget(LivingEntity entity) {
            var loc = Random.insideUnitSphere * _radius;
            loc.y = 1;
            _target = loc;
        }

        private void Rotate(LivingEntity entity, Vector3 dir) {
            dir.y = 1;
            entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, Quaternion.LookRotation(dir),
                _rotationSpeed*Time.deltaTime);
        }
    }
}
