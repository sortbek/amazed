using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class EntityWanderBehaviour : AbstractEntityBehaviour {

        private readonly float _speed, _triggerDistance, _radius;
        private Vector3? _target;

        public EntityWanderBehaviour(LivingEntity entity) : base(entity) {
            _speed = 1f;
            _radius = 10;
            _triggerDistance = .3f;
            _target = null;
        }

        public override Vector3 Update() {
            if (_target == null || Vector3.Distance(Entity.transform.position, _target.Value) < _triggerDistance)
                UpdateTarget();
            Rotate(Entity, _target.Value);
            return Vector3.MoveTowards(Entity.transform.position, _target.Value, _speed * Time.deltaTime);
        }

        private void UpdateTarget() {
            var loc = Random.insideUnitSphere * _radius;
            loc.y = 1;
            _target = loc;
        }
    }
}