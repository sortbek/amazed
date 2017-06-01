using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    class PathFollowingBehaviour : IEntityBehaviour {
        
        public Vector3[] Path;
        public int CurrentIndex;

        public PathFollowingBehaviour() {
            
        }
        public Vector3 Update(LivingEntity entity) {
            if (Path != null && Path.Length > 0) {
                entity.PlayAnimation(Animation.run);
                Path[CurrentIndex].y = 0.0f;

                entity.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Agent.transform.forward, Path[CurrentIndex] - Agent.transform.position,
                    Time.deltaTime * entity.Speed, 0.0f));

                if (CurrentIndex == Path.Length - 1) Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
                if (Vector3.Distance(Agent.transform.position, Path[CurrentIndex]) < 0.1f) CurrentIndex += 1;

                return Vector3.MoveTowards(entity.transform.position, Path[CurrentIndex], Time.deltaTime * entity.Speed);
            }
            entity.StateMachine.ChangeState(GoapStateMachine.StateType.Action);
        }
    }
}
