using Assets.Scripts.AI.GOAP;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    internal class PathFollowingBehaviour : AbstractEntityBehaviour {
        public Vector3[] Path;

        public PathFollowingBehaviour(LivingEntity entity) : base(entity) { }
        public int CurrentIndex { get; set; }

        public override Vector3 Update() {
            if (Path == null || Path.Length <= 0) return Entity.transform.position;

            Entity.PlayAnimation(Animation.run);
            Path[CurrentIndex].y = 0.0f;

            var target = Vector3.MoveTowards(Entity.transform.position, Path[CurrentIndex],
                Time.deltaTime * Entity.Speed);

            Rotate(Entity, target);

            if (Vector3.Distance(Entity.transform.position, Path[CurrentIndex]) < 0.1f) CurrentIndex += 1;

            return target;
        }

        public void UpdateTarget() {
            CurrentIndex = 0;
            var agent = Entity.GetComponent<GoapAgent>();
            var target = agent.ActionQueue.Peek().GetTarget() == null
                ? Entity.transform.position
                : agent.ActionQueue.Peek().GetTarget();

            PathRequestManager.RequestPath(Entity.transform.position, target.Value, OnPathFound);
        }

        public void OnPathFound(Vector3[] newPath, bool pathFound) {
            if (pathFound) Path = newPath;
        }

        public bool Reached() {
            return Vector3.Distance(Entity.transform.position, GameManager.Instance.Character.transform.position) <
                   0.3f;
        }
    }
}