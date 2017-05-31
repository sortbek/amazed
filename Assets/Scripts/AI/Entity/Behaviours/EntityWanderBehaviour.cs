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

        private float _speed, _radius, _arriveRadius;
        private Vector3? _target;

        public void Load(LivingEntity entity) {
            _speed = .4f * Time.deltaTime;
            _arriveRadius = .5f;
            _radius = 4f;
            _target = null;
        }

        public Vector3 Update(LivingEntity entity) {
            if (_target == null || Vector3.Distance(entity.transform.position, _target.Value) < _arriveRadius)
                _target = Fetch(entity.transform.position);
            return Vector3.Lerp(entity.transform.position, _target.Value, _speed);
        }

        private Vector3 Fetch(Vector3 current) {
            var ranx = Random.Range(-_radius, _radius);
            var ranz = Random.Range(-_radius, _radius);
            return new Vector3(current.x +ranx, current.y, current.z+ranz);
        }

    }
}
