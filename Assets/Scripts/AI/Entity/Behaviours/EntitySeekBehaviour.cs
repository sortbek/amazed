using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class EntitySeekBehaviour : AbstractEntityBehaviour {
        private const float DistanceOffset = 3.3f;

        private Vector3? _target;

        public EntitySeekBehaviour(LivingEntity entity) : base(entity) { }

        public void UpdateTarget(Vector3 target) {
            _target = target;
        }

        public override Vector3 Update() {
            if (_target == null)
                return Entity.transform.position;
            Entity.PlayAnimation(Animation.Run);
            var target = _target.Value;
            target.y = 0;
            Entity.Rotate(target);
            var current = Entity.transform.position;
            return Vector3.MoveTowards(current, target, Entity.Speed * Time.deltaTime);
        }

        public bool Found() {
            return _target != null && Vector3.Distance(Entity.transform.position, _target.Value) < DistanceOffset;
        }
    }
}