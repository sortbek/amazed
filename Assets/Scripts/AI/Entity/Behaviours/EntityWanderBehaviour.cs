using Assets.Scripts.PathFinding;
using Assets.Scripts.World;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.AI.Entity.Behaviours {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class EntityWanderBehaviour : AbstractEntityBehaviour {
        private readonly EntityPathFollowingBehaviour _pathFollowing;
        private readonly Random _random;
        private Vector3 _currentTarget;

        private Grid _grid;

        public EntityWanderBehaviour(LivingEntity entity) : base(entity) {
            _pathFollowing = new EntityPathFollowingBehaviour(entity, .8f, Animation.Walk);
            _random = new Random();
            _grid = null;
        }

        public void Reset() {
            _currentTarget = Entity.transform.position;
        }

        public override Vector3 Update() {
            if (_pathFollowing.CurrentRequest != null && !_pathFollowing.Reached())
                return _pathFollowing.Update();
            UpdateLocation();
            _pathFollowing.UpdateRequest(_currentTarget);
            return _pathFollowing.Update();
        }

        private void UpdateLocation() {
            var game = GameManager.Instance;
            if (_grid == null) {
                var pathComponent = GameObject.Find("World").transform.Find("Pathfinding");
                _grid = pathComponent.GetComponent<Grid>();
            }
            Node found = null;
            while (found == null) {
                var content = _grid.GetGrid();
                var n = content[_random.Next(10, content.GetLength(0) - 10),
                    _random.Next(10, content.GetLength(1) - 10)];
                found = n.Walkable && n.WorldPosition != game.GetStartPoint() && n.WorldPosition != game.GetEndpoint()
                    ? n
                    : null;
            }
            _currentTarget = found.WorldPosition;
        }
    }
}