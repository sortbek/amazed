using Assets.Scripts.AI.GOAP;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public class EntityPathFollowingBehaviour : AbstractEntityBehaviour {

        public Vector3[] Path { get; private set; }
        public int CurrentIndex { get; set; }
        public Vector3? CurrentRequest { get; private set; }

        private readonly float _speed;
        private bool _reached;
        private readonly Animation _animation;

        public EntityPathFollowingBehaviour(LivingEntity entity) : this(entity, entity.Speed, Animation.Run) {}
        public EntityPathFollowingBehaviour(LivingEntity entity, float speed, Animation animation) : base(entity) {
            _speed = speed;
            _reached = false;
            CurrentRequest = null;
            _animation = animation;
        }

        public override Vector3 Update() {
            if (Path == null || Path.Length <= 0)
                return Entity.transform.position;
            Entity.PlayAnimation(_animation);
            Vector3 target;
            if (CurrentIndex == Path.Length) {
                target = CurrentRequest.Value;
                _reached = true;
            } else {
                target = Path[CurrentIndex];
                Path[CurrentIndex].y = 0.0f;
                if (Vector3.Distance(Entity.transform.position, target) < 0.1f)
                    CurrentIndex += 1;
            }
            target.y = 0.0f;
            var destination = Vector3.MoveTowards(Entity.transform.position, target,
                Time.deltaTime * _speed);
            Entity.Rotate(destination);
            return destination;
        }

        public static int A = 0;
        public void UpdateRequest(Vector3 target) {
            Debug.Log("Request change pathfinding -> " + (++A));
            CurrentIndex = 0;
            _reached = false;
            CurrentRequest = target;
            PathRequestManager.RequestPath(Entity.transform.position, target, OnPathFound);
        }

        private void OnPathFound(Vector3[] newPath, bool pathFound) {
            if (pathFound)
                Path = newPath;
            else Debug.Log("couldn't find path to "+ CurrentRequest);
        }

        public bool Reached() {
            return _reached;
        }
    }
}