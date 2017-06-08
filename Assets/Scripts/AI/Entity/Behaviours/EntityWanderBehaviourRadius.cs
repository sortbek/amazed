using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class EntityWanderBehaviourRadius : AbstractEntityBehaviour {

        private readonly float _speed, _triggerDistance, _radius;
        private Vector3? _target, _startWanderPosition;

        public EntityWanderBehaviourRadius(LivingEntity entity) : base(entity) {
            _speed = 1f;
            _radius = 3;
            RotationSpeed = 5f;
            _triggerDistance = .3f;
            Reset();
        }

        public void Reset() {
            _target = null;
            _startWanderPosition = null;
        }

        public override Vector3 Update() {
            if (_startWanderPosition == null)
                _startWanderPosition = Entity.transform.position;
            if (_target == null || Vector3.Distance(Entity.transform.position, _target.Value) < _triggerDistance)
                UpdateTarget();
            Entity.Rotate(_target.Value);
            return Vector3.MoveTowards(Entity.transform.position, _target.Value, _speed * Time.deltaTime);
        }

        private void UpdateTarget() {
            var loc = Random.insideUnitSphere * _radius;
            loc += Entity.transform.position;
            loc.y = Entity.transform.position.y;
            _target = Vector3.Distance(_startWanderPosition.Value, loc) > _radius*2 ? _startWanderPosition.Value : loc;
        }
    }
}